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
        int[][] gamePlan, solution;
        List<SudokuCell> sudokuCells;
        ISudokuInput sudokuInput = null;
        List<ISudokuOutput> sudokuOutputs = new List<ISudokuOutput>();
        string info;

        int blockSize = defaultBlockSize, cellsToSolve = 0;
        const int widthHeight = 9, defaultBlockSize = 3;

        public Sudoku(string gamePlanString)
        {
            if (gamePlanString.Length != widthHeight * widthHeight)
                throw new ArgumentOutOfRangeException($"The length of string must be {widthHeight * widthHeight} characters.");

            int[][] gamePlan = new int[widthHeight][];

            for (int i = 0; i < gamePlanString.Length; i++)
            {
                int row = i / widthHeight;
                int col = i % widthHeight;
                if (col == 0)
                    gamePlan[row] = new int[widthHeight];


                if (!int.TryParse(gamePlanString[i].ToString(), out int number))
                    throw new ArgumentException("All characters in the string must be numbers.");
                else
                    gamePlan[row][col] = number;
            }
            InitSudoku(gamePlan, blockSize);
        }

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

        public string BoardAsText
        {
            get
            {
                if (this.solution == null)
                    throw new NullReferenceException("There is no solution available. Be sure to call method Solve() before calling method OutputSolution()");

                StringBuilder stringBuilder = new StringBuilder();

                for (int row = 0; row < solution.Length; row++)
                {
                    if (row % 3 == 0)
                        stringBuilder.AppendLine(new string('-', widthHeight * 2 + 3));
                    for (int col = 0; col < solution[row].Length; col++)
                    {
                        if (col > 0 && col % 3 == 0)
                            stringBuilder.Append("| ");
                        stringBuilder.Append($"{solution[row][col]} ");
                    }
                    stringBuilder.AppendLine();
                }
                stringBuilder.AppendLine(new string('-', widthHeight * 2 + 3));

                if (info != null)
                    stringBuilder.AppendLine($"\r\nInfo: {info}");

                return stringBuilder.ToString();
            }
        }

        
        public List<ISudokuOutput> SudokuOutputs
        {
            set
            {
                this.sudokuOutputs = value;
            }
        }

        public ISudokuOutput SudokuOutput
        {
            set
            {
                this.sudokuOutputs.Add(value);
            }
        }

        public ISudokuInput SudokuInput
        {
            set
            {
                this.sudokuInput = value;
            }
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
                        {
                            this.solution = resultNewGamePlan;
                            return resultNewGamePlan;
                        }

                    }
                    return null;
                }
            }

            this.solution = gamePlanSolved;

            return gamePlanSolved;

        }

        public void OutputSolution()
        {
            if (this.solution == null)
                throw new NullReferenceException("There is no solution available. Be sure to call method Solve() before calling method OutputSolution()");
            if (sudokuOutputs.Count > 0)
                foreach (ISudokuOutput sudokuOutput in sudokuOutputs)
                    sudokuOutput.Output(this.solution, this.info);
            else
                throw new NullReferenceException("There is no ISudokuOutput assigned to this sudoku.");
        }

        public void OutputGameplan()
        {
            if (this.sudokuOutputs.Count == 0)
                throw new NullReferenceException("There is no ISudokuOutput assigned to this sudoku.");

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
