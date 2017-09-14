using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class ConsoleOutput:ISudokuOutput
    {
        public void Output(int[][] gamePlan, string info=null)
        {
            foreach (var row in gamePlan)
            {
                Console.Write("|");
                foreach (var item in row)
                {
                    Console.Write($" {item} |");                  
                }
                Console.WriteLine();
            }
            if (info != null)
                Console.WriteLine($"\r\nInfo: {info}");
        }

       
    }
}
