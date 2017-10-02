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
            //Sudoku sudoku5 = new Sudoku("070006950" +
            //                            "002000048" +
            //                            "000090703" +
            //                            "000960005" +
            //                            "050000020" +
            //                            "200081000" +
            //                            "406030000" +
            //                            "720000500" +
            //                            "018600070");
            ConsoleOutput consoleOutput = new ConsoleOutput();

            //SudokuKryssInput sudokuKryssInput = new SudokuKryssInput();
            //Sudoku sudokuDifficult = new Sudoku("530070000" +
            //                                  "600195000" +
            //                                  "098000060" +
            //                                  "800060003" +
            //                                  "400803001" +
            //                                  "700020006" +
            //                                  "060000280" +
            //                                  "000419005" +
            //                                  "000080079");

            Sudoku medel = new Sudoku("037060000205000800006908000" +
                                      "000600024001503600650009000" +
                                      "000302700009000402000050360");
            //Sudoku sudokukryss = new Sudoku(3, sudokuKryssInput, consoleOutput);

            //Sudoku sudoku6 = new Sudoku("003020600900305001001806400" +
            //                          "008102900700000008006708200" +
            //                          "002609500800203009005010300");

            Console.WriteLine("\r\n====Solution for sudoku-kryss:====");
            medel.Solve();
            Console.WriteLine("\r\n\r\n" + medel.BoardAsText);
            Console.ReadKey();
        }
    }
}
