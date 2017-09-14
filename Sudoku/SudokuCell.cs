using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class SudokuCell
    {
        List<int> numbers;
        int x, y;

        public SudokuCell(List<int> numbers, int x, int y)
        {
            this.numbers = numbers;
            this.x = x;
            this.y = y;
        }

        public int X
        {
            get { return x; }

        }

        public int Y
        {
            get { return y; }

        }

        public List<int> Numbers
        {
            get { return numbers; }
        }

        public void ReduceNumbers(int[] collection)
        {
            List<int> tempNumbers = new List<int>(numbers);

            for (int i = 0; i < numbers.Count; i++)
            {
                int number = numbers[i];

                bool existsInCollection = collection.Any(numberInCollection => numberInCollection == number);

                if (existsInCollection)
                    tempNumbers.Remove(number);
            }

            numbers = tempNumbers;

        }

    }
}
