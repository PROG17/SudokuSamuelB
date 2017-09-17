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
            Sudoku sudoku5 = new Sudoku("070006950" +
                                        "002000048" +
                                        "000090703" +
                                        "000960005" +
                                        "050000020" +
                                        "200081000" +
                                        "406030000" +
                                        "720000500" +
                                        "018600070");

            Console.WriteLine("\r\n====Solution for sudoku-kryss:====");
            sudoku5.Solve();
            Console.WriteLine("\r\n\r\n" + sudoku5.BoardAsText);
            Console.ReadKey();
        }
    }
}
