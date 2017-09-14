using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Sudoku
    {
        int[][] gamePlan;
        List<SudokuCell> sudokuCells;
        ISudokuInput sudokuInput = null;
        List<ISudokuOutput> sudokuOutputs = new List<ISudokuOutput>();
        string info;

        int blockSize, cellsToSolve = 0;

        public Sudoku(int[][] gamePlan, int blockSize, ISudokuOutput sudokuOutput)
        {
            this.sudokuOutputs.Add(sudokuOutput);
            InitSudoku(gamePlan, blockSize);
        }

        public Sudoku(int[][] gamePlan, int blockSize, List<ISudokuOutput> sudokuOutputs)
        {
            this.sudokuOutputs = sudokuOutputs;
            InitSudoku(gamePlan, blockSize);
        }

        public Sudoku(int blockSize, ISudokuInput sudokuInput, ISudokuOutput sudokuOutput)
        {
            this.sudokuInput = sudokuInput;
            int[][] gamePlan = this.sudokuInput.GetData(out this.info);
            this.sudokuOutputs.Add(sudokuOutput);
            InitSudoku(gamePlan, blockSize);
        }

        public Sudoku(int blockSize, ISudokuInput sudokuInput, List<ISudokuOutput> sudokuOutputs)
        {
            this.sudokuInput = sudokuInput;
            int[][] gamePlan = this.sudokuInput.GetData(out this.info);
            this.sudokuOutputs = sudokuOutputs;
            InitSudoku(gamePlan, blockSize);
        }

        private Sudoku(int[][] gamePlan, ISudokuInput sudokuInput, int blockSize, List<ISudokuOutput> sudokuOutputs)
        {
            this.sudokuInput = sudokuInput;
            this.sudokuOutputs = sudokuOutputs;
            InitSudoku(gamePlan, blockSize);
        }

        private void InitSudoku(int[][] gamePlan, int blockSize)
        {
            this.gamePlan = gamePlan;
            this.blockSize = blockSize;

            sudokuCells = new List<SudokuCell>();

            for (int i = 0; i < gamePlan.Length; i++)
            {
                for (int j = 0; j < gamePlan[i].Length; j++)
                {
                    if (gamePlan[i][j] == 0)
                    {
                        sudokuCells.
                            Add(new SudokuCell(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, j, i));
                        cellsToSolve++;
                    }
                }
            }
        }


        public int[][] Solve()
        {
            //int[][] gamePlanSolved = (int[][])gamePlan.Clone();
            int[][] gamePlanSolved = CustomDeepCopy.CreateJaggedArrayCopy(gamePlan);

            while (cellsToSolve > 0)
            {
                int previousCellsToSolve = cellsToSolve;

                for (int i = 0; i < sudokuCells.Count; i++)
                {
                    sudokuCells[i].ReduceNumbers(GetRow(sudokuCells[i].Y, gamePlanSolved));
                    sudokuCells[i].ReduceNumbers(GetColumn(sudokuCells[i].X, gamePlanSolved));
                    int block = sudokuCells[i].X / blockSize + (sudokuCells[i].Y / blockSize) * blockSize;
                    sudokuCells[i].ReduceNumbers(GetBlock(block, gamePlanSolved));

                    if (sudokuCells[i].Numbers.Count == 1)
                    {
                        gamePlanSolved[sudokuCells[i].Y][sudokuCells[i].X] = sudokuCells[i].Numbers[0];
                        cellsToSolve--;
                        sudokuCells.RemoveAt(i);
                        i--;
                    }
                    else if (sudokuCells[i].Numbers.Count == 0)    //bottom case of recursion 
                        return null;

                }
                //recursion needed 
                if (cellsToSolve == previousCellsToSolve)
                {
                    foreach (int number in sudokuCells[0].Numbers)
                    {
                        //int[][] newGamePlan = (int[][])gamePlanSolved.Clone();
                        int[][] newGamePlan = CustomDeepCopy.CreateJaggedArrayCopy(gamePlanSolved);

                        newGamePlan[sudokuCells[0].Y][sudokuCells[0].X] = number;
                        Sudoku newSudoku = new Sudoku(newGamePlan, this.sudokuInput, this.blockSize, this.sudokuOutputs);
                        int[][] resultNewGamePlan = newSudoku.Solve();
                        if (resultNewGamePlan != null)
                            return resultNewGamePlan;

                    }
                    return null;
                }
            }

            foreach (ISudokuOutput sudokuOutput in sudokuOutputs)
                sudokuOutput.Output(gamePlanSolved, this.info);

            return gamePlanSolved;

        }

        public void OutputGameplan()
        {
            foreach (ISudokuOutput sudokuOutput in sudokuOutputs)
                sudokuOutput.Output(gamePlan, this.info);
        }

        private int[] GetRow(int row, int[][] gamePlan)
        {
            return gamePlan[row];
        }

        private int[] GetColumn(int column, int[][] gamePlan)
        {
            return gamePlan.Select(row => row[column]).ToArray();
        }

        private int[] GetBlock(int block, int[][] gamePlan)
        {
            int blocksPerRow = gamePlan[0].Length / blockSize;
            int blocksPerColumn = gamePlan.GetLength(0) / blockSize;

            var rowsInBlock = gamePlan.Where((row, index) =>
            {
                int startRow = (block / blocksPerRow) * blocksPerRow;
                return (index >= startRow && index < startRow + blocksPerColumn);
            });

            var cellsInBlock = rowsInBlock.Select(row =>
            {
                var cells = row.Where((cell, index) =>
                  {
                      int startColumn = block * blockSize % (blockSize * blocksPerRow);
                      return (index >= startColumn && index < startColumn + blockSize);
                  });

                return cells.ToArray();

            });

            int[] cellsArray = cellsInBlock.SelectMany(i => i).ToArray();

            return cellsArray;
        }
    }
}
