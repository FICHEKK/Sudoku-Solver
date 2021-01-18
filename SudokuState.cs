using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    public class SudokuState
    {
        private const char VerticalDividerSymbol = '│';
        private const char HorizontalDividerSymbol = '-';

        private const int RowDimension = 0;
        private const int ColumnDimension = 0;

        public int FieldDimension { get; }
        private readonly int _boxDimension;
        private readonly int[,] _field;

        public SudokuState(int[,] field)
        {
            var rows = field.GetLength(RowDimension);
            var columns = field.GetLength(ColumnDimension);

            if (rows != columns)
                throw new ArgumentException($"Row dimension ({rows}) != Column dimension ({columns})");

            FieldDimension = rows;
            _boxDimension = (int) Math.Sqrt(FieldDimension);
            _field = field;
        }

        public (int row, int column) FirstEmptyPosition
        {
            get
            {
                for (var row = 0; row < FieldDimension; row++)
                {
                    for (var column = 0; column < FieldDimension; column++)
                    {
                        if (_field[row, column] == 0)
                        {
                            return (row, column);
                        }
                    }
                }

                return (-1, -1);
            }
        }

        public int MissingNumbersCount
        {
            get
            {
                var missingNumbersCount = 0;

                for (var row = 0; row < FieldDimension; row++)
                {
                    for (var column = 0; column < FieldDimension; column++)
                    {
                        if (_field[row, column] == 0) missingNumbersCount++;
                    }
                }

                return missingNumbersCount;
            }
        }

        public bool IsValid => !DoesAnyRowContainDuplicate() && !DoesAnyColumnContainDuplicate() && !DoesAnyBoxContainDuplicate();

        private bool DoesAnyRowContainDuplicate()
        {
            for (var row = 0; row < FieldDimension; row++)
            {
                var rowNumbers = new HashSet<int>();

                for (var column = 0; column < FieldDimension; column++)
                {
                    var number = _field[row, column];
                    if (number == 0) continue;
                    if (rowNumbers.Contains(number)) return true;
                    rowNumbers.Add(number);
                }
            }

            return false;
        }

        private bool DoesAnyColumnContainDuplicate()
        {
            for (var column = 0; column < FieldDimension; column++)
            {
                var columnNumbers = new HashSet<int>();

                for (var row = 0; row < FieldDimension; row++)
                {
                    var number = _field[row, column];
                    if (number == 0) continue;
                    if (columnNumbers.Contains(number)) return true;
                    columnNumbers.Add(number);
                }
            }

            return false;
        }

        private bool DoesAnyBoxContainDuplicate()
        {
            for (var boxRow = 0; boxRow < _boxDimension; boxRow++)
            for (var boxColumn = 0; boxColumn < _boxDimension; boxColumn++)
            {
                var boxNumbers = new HashSet<int>();

                for (var rowOffset = 0; rowOffset < _boxDimension; rowOffset++)
                for (var columnOffset = 0; columnOffset < _boxDimension; columnOffset++)
                {
                    var row = boxRow * _boxDimension + rowOffset;
                    var column = boxColumn * _boxDimension + columnOffset;
                    var number = _field[row, column];

                    if (number == 0) continue;
                    if (boxNumbers.Contains(number)) return true;
                    boxNumbers.Add(number);
                }
            }

            return false;
        }

        public SudokuState PutNumber(int row, int column, int number)
        {
            var field = (int[,]) _field.Clone();
            field[row, column] = number;
            return new SudokuState(field);
        }

        public bool CheckNumber(int row, int column, int number)
        {
            var boxRow = row / _boxDimension;
            var boxColumn = column / _boxDimension;
            return IsRowValid(row, number) && IsColumnValid(column, number) && IsBoxValid(boxRow, boxColumn, number);
        }

        private bool IsRowValid(int row, int number)
        {
            for (var column = 0; column < FieldDimension; column++)
            {
                if (_field[row, column] == number) return false;
            }

            return true;
        }

        private bool IsColumnValid(int column, int number)
        {
            for (var row = 0; row < FieldDimension; row++)
            {
                if (_field[row, column] == number) return false;
            }

            return true;
        }

        private bool IsBoxValid(int boxRow, int boxColumn, int number)
        {
            for (var rowOffset = 0; rowOffset < _boxDimension; rowOffset++)
            {
                for (var columnOffset = 0; columnOffset < _boxDimension; columnOffset++)
                {
                    var row = boxRow * _boxDimension + rowOffset;
                    var column = boxColumn * _boxDimension + columnOffset;
                    if (_field[row, column] == number) return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var row = 0; row < FieldDimension; row++)
            {
                for (var column = 0; column < FieldDimension; column++)
                {
                    var number = _field[row, column];
                    sb.Append(number == 0 ? "  " : $"{number,2}");

                    if (column < FieldDimension - 1)
                    {
                        sb.Append(' ');
                        if ((column + 1) % _boxDimension == 0) sb.Append(VerticalDividerSymbol).Append(' ');
                    }
                }

                if (row < FieldDimension - 1)
                {
                    sb.Append(Environment.NewLine);
                    if ((row + 1) % _boxDimension == 0) sb.Append(HorizontalDivider).Append(Environment.NewLine);
                }
            }

            return sb.ToString();
        }

        private string HorizontalDivider => new string(HorizontalDividerSymbol, FieldDimension * 3 - 1 + (_boxDimension - 1) * 2);
    }
}