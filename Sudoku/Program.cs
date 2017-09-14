using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {

            int[][] sudoku1Template = new int[][]
            {
              new int[]  { 8,0,0,3,0,1,5,0,0 },
              new int[]  { 1,0,5,6,4,0,9,0,0 },
              new int[]  { 0,2,0,0,0,0,0,0,0 },
              new int[]  { 0,0,8,0,0,0,1,0,3 },
              new int[]  { 4,6,0,0,0,0,0,9,7 },
              new int[]  { 0,5,0,0,1,0,6,0,0 },
              new int[]  { 0,0,0,0,0,8,0,0,9 },
              new int[]  { 9,0,6,1,0,2,4,7,0 },
              new int[]  { 0,0,3,5,0,6,8,1,2 },
            };

            int[][] sudoku2Template = new int[][]
            {
              new int[]  { 4,0,7,0,0,0,3,0,0 },
              new int[]  { 0,9,3,0,2,8,5,0,0 },
              new int[]  { 0,5,0,1,0,3,0,4,0 },
              new int[]  { 9,8,0,5,0,0,0,7,0 },
              new int[]  { 0,0,0,6,9,7,0,0,0 },
              new int[]  { 0,7,0,0,0,4,0,1,6 },
              new int[]  { 0,6,0,2,0,9,0,3,0 },
              new int[]  { 0,0,9,3,5,0,7,2,0 },
              new int[]  { 0,0,4,0,0,0,6,0,5 },
            };

            ConsoleOutput consoleOutput = new ConsoleOutput();
            //FileOutput fileOutput = new FileOutput("sudoku1.txt");
            //FileOutput fileOutput2 = new FileOutput("sudoku2.txt");
            FileOutput fileOutputSudokuKryss = new FileOutput("SudokuKryss.txt");

            //FileInput fileInput = new FileInput("sudoku1.txt");
            //FileInput fileInput2 = new FileInput("sudoku2.txt");
            //FileInput fileInput3 = new FileInput("sudoku3.txt");
            //FileInput fileInput4 = new FileInput("sudoku4.txt");
            SudokuKryssInput sudokuKryssInput = new SudokuKryssInput();

            //Sudoku sudoku1 = new Sudoku(3, fileInput, consoleOutput);
            //Sudoku sudoku2 = new Sudoku(3, fileInput2, consoleOutput);
            //Sudoku sudoku3 = new Sudoku(3, fileInput3, consoleOutput);
            //Sudoku sudoku4 = new Sudoku(3, fileInput4, consoleOutput);
            Sudoku sudokuKryss = new Sudoku(3, sudokuKryssInput,
                new List<ISudokuOutput> { consoleOutput, fileOutputSudokuKryss });

            //testfileinput.GetData();
            //Console.ReadKey();
            //Console.WriteLine("====Gameplan 1====\r\n");
            //sudoku1.OutputGameplan();

            //Console.WriteLine("====Gameplan 2====\r\n");
            //sudoku2.OutputGameplan();

            //Console.WriteLine("====Gameplan 3====\r\n");
            //sudoku3.OutputGameplan();

            //Console.ReadKey();

            //Console.WriteLine("====Sudoku 4====");
            //sudoku4.OutputGameplan();
            //Console.ReadKey();

            Console.WriteLine("====Sudokukryss====");
            sudokuKryss.OutputGameplan();

            Console.ReadKey();

            //Console.WriteLine("====Solution for sudoku 1:====");
            //sudoku1.Solve();
            //Console.WriteLine("\r\n\r\n");

            //Console.WriteLine("====Solution for sudoku 2:====");
            //sudoku2.Solve();

            //Console.ReadKey();
            //Console.WriteLine("====Solution for sudoku 3:====");
            //sudoku3.Solve();

            //Console.ReadKey();
            //Console.WriteLine("\r\n====Solution for sudoku 4:====");
            //sudoku4.Solve();
            //Console.ReadKey();

            Console.WriteLine("\r\n====Solution for sudoku-kryss:====");
            sudokuKryss.Solve();
            Console.ReadKey();



        }
    }
}
