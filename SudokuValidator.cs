using System.Collections.Generic;

namespace SudokuSolver
{
    public static class SudokuValidator
    {
        public static bool Validate(Sudoku sudoku) =>
            NoRowContainsDuplicates(sudoku) &&
            NoColumnContainsDuplicates(sudoku) &&
            NoBoxContainsDuplicates(sudoku);

        private static bool NoRowContainsDuplicates(Sudoku sudoku)
        {
            for (var row = 0; row < sudoku.FieldDimension; row++)
            {
                var rowNumbers = new HashSet<int>();

                for (var column = 0; column < sudoku.FieldDimension; column++)
                {
                    var number = sudoku[row, column];
                    if (number == Sudoku.EmptyCell) continue;
                    if (rowNumbers.Contains(number)) return false;
                    rowNumbers.Add(number);
                }
            }

            return true;
        }

        private static bool NoColumnContainsDuplicates(Sudoku sudoku)
        {
            for (var column = 0; column < sudoku.FieldDimension; column++)
            {
                var columnNumbers = new HashSet<int>();

                for (var row = 0; row < sudoku.FieldDimension; row++)
                {
                    var number = sudoku[row, column];
                    if (number == Sudoku.EmptyCell) continue;
                    if (columnNumbers.Contains(number)) return false;
                    columnNumbers.Add(number);
                }
            }

            return true;
        }

        private static bool NoBoxContainsDuplicates(Sudoku sudoku)
        {
            for (var boxRow = 0; boxRow < sudoku.BoxDimension; boxRow++)
            for (var boxColumn = 0; boxColumn < sudoku.BoxDimension; boxColumn++)
            {
                var boxNumbers = new HashSet<int>();

                for (var rowOffset = 0; rowOffset < sudoku.BoxDimension; rowOffset++)
                for (var columnOffset = 0; columnOffset < sudoku.BoxDimension; columnOffset++)
                {
                    var row = boxRow * sudoku.BoxDimension + rowOffset;
                    var column = boxColumn * sudoku.BoxDimension + columnOffset;
                    var number = sudoku[row, column];

                    if (number == Sudoku.EmptyCell) continue;
                    if (boxNumbers.Contains(number)) return false;
                    boxNumbers.Add(number);
                }
            }

            return true;
        }
    }
}