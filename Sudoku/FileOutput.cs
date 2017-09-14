using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class FileOutput : ISudokuOutput
    {
        string filename;

        public FileOutput(string filename)
        {
            this.filename = filename;
        }

        public void Output(int[][] gamePlan, string info=null)
        {
            StreamWriter writer = new StreamWriter(filename);
            // Ensure the writer will be closed when no longer used
            using (writer)
            {
                writer.WriteLine("Sudoku-file");

                for (int row = 0; row < gamePlan.Length; row++)
                {
                    for (int col = 0; col < gamePlan[row].Length; col++)
                    {
                        writer.Write(gamePlan[row][col]);
                    }
                    writer.WriteLine();
                }

                if (info != null)
                    writer.WriteLine($"Info\r\n{info}");
            }
        }
    }
}
