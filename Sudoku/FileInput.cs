using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class FileInput : ISudokuInput
    {
        string filename;

        public FileInput(string filename)
        {
            this.filename = filename;
        }

        public int[][] GetData(out string info)
        {

            List<List<int>> gamePlanList = new List<List<int>>();
            int[][] result;

            StreamReader reader = new StreamReader(filename);
            using (reader)
            {
                // Read first line from the text file
                string line = reader.ReadLine();
                if (line != "Sudoku-file")
                    throw new Exception("Not Sudoku-file!");

                // Read the other lines from the text file
                line = reader.ReadLine();
                int i = 0;
                while (line != null && line != "Info")
                {

                    gamePlanList.Add(new List<int>());

                    foreach (char ch in line)
                    {
                        int num;
                        if (!int.TryParse("" + ch, out num))
                            throw new Exception("Sudokufile contains invalid data");
                        gamePlanList[i].Add(num);

                    }
                    i++;
                    line = reader.ReadLine();

                }
                if (line == "Info")
                    info = reader.ReadToEnd();
                else
                    info = null;
            }
            result = gamePlanList.Select(listOfInt => listOfInt.ToArray()).ToArray();

            return result;
        }
    }
}
