﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public interface ISudokuOutput
    {
        void Output(int[][] gamePlan, string info);
    }
}
