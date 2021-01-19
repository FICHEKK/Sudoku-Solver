using System;
using System.Text;

namespace SudokuSolver
{
    public static class SudokuStringifier
    {
        private const char VerticalDividerSymbol = '|';
        private const char HorizontalDividerSymbol = '-';

        public static string Stringify(Sudoku sudoku, int canvasWidth)
        {
            var sudokuBuilder = new StringBuilder();
            var horizontalDivider = CalculateHorizontalDivider(sudoku);
            var indentation = new string(' ', (canvasWidth - CalculateSudokuWidthInCharacters(sudoku)) / 2);

            for (var row = 0; row < sudoku.FieldDimension; row++)
            {
                sudokuBuilder.Append(indentation);

                if (row % sudoku.BoxDimension == 0)
                {
                    sudokuBuilder.Append(horizontalDivider);
                    sudokuBuilder.Append(Environment.NewLine);
                    sudokuBuilder.Append(indentation);
                }

                sudokuBuilder.Append(StringifyRow(sudoku, row));
                sudokuBuilder.Append(Environment.NewLine);
            }

            sudokuBuilder.Append(indentation);
            sudokuBuilder.Append(horizontalDivider);

            return sudokuBuilder.ToString();
        }

        private static string StringifyRow(Sudoku sudoku, int row)
        {
            var rowBuilder = new StringBuilder();

            for (var column = 0; column < sudoku.FieldDimension; column++)
            {
                if (column % sudoku.BoxDimension == 0)
                {
                    rowBuilder.Append(VerticalDividerSymbol);
                    rowBuilder.Append(' ');
                }

                var number = sudoku[row, column];
                rowBuilder.Append(number == Sudoku.EmptyCell ? "  " : $"{number,2}");
                rowBuilder.Append(' ');
            }

            rowBuilder.Append(VerticalDividerSymbol);

            return rowBuilder.ToString();
        }

        private static string CalculateHorizontalDivider(Sudoku sudoku) =>
            new string(HorizontalDividerSymbol, CalculateSudokuWidthInCharacters(sudoku));

        private static int CalculateSudokuWidthInCharacters(Sudoku sudoku)
        {
            var numbersLength = sudoku.FieldDimension * 3;
            var dividersLength = (sudoku.BoxDimension + 1) * 2 - 1;
            return numbersLength + dividersLength;
        }
    }
}