using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;

namespace SudokuTest
{
    [TestClass]
    public class SudokuTests
    {
        [TestMethod]
        public void SimpleSudokuReturnSolution()
        {
            //Arrange
            Sudoku.Sudoku sudoku = new Sudoku.Sudoku("305420810487901506029056374850793041613208957074065280241309065508670192096512408");
            //Act
            sudoku.Solve();
            var actual = sudoku.BoardAsText.Replace("-", "");
            actual = actual.Replace("|", "");
            actual = actual.Replace("\r", "");
            actual = actual.Replace("\n", "");
            actual = actual.Replace(" ", "");


            string expected = "365427819487931526129856374852793641613248957974165283241389765538674192796512438";
            
            //Assert
            Assert.AreEqual(expected, actual);          
        }

        [TestMethod]
        public void MediumSudokuReturnSolution()
        {
            //Arrange
            Sudoku.Sudoku sudoku = new Sudoku.Sudoku("002030008000008000031020000060050270010000050204060031000080605000000013005310400");
            //Act
            sudoku.Solve();
            var actual = sudoku.BoardAsText.Replace("-", "");
            actual = actual.Replace("|", "");
            actual = actual.Replace("\r", "");
            actual = actual.Replace("\n", "");
            actual = actual.Replace(" ", "");


            string expected = "672435198549178362831629547368951274917243856254867931193784625486592713725316489";

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UnsolveableSudokuReturnNoSolution()
        {
            //Arrange
            Sudoku.Sudoku sudoku = new Sudoku.Sudoku("090300001000080046000000800405060030003275600060010904001000000580020000200007060");
            //Act
            sudoku.Solve();
            var actual = sudoku.BoardAsText;
            

            string expected = "(no solution)";

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
