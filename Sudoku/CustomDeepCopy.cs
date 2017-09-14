using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class CustomDeepCopy
    {
        public static int[][] CreateJaggedArrayCopy(int[][] gamePlan)
        {
            int[][] copy = new int[gamePlan.Length][];
            for (int i = 0; i < gamePlan.Length; i++)
            {
                copy[i] = new int[gamePlan[i].Length];

                for (int j = 0; j < gamePlan[i].Length; j++)
                    copy[i][j] = gamePlan[i][j];

            }
            return copy;
        }
    }
}
