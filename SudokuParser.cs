using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SudokuSolver
{
    public static class SudokuParser
    {
        private const string CommentSymbol = "#";
        private const string HorizontalDividerSymbol = "-";
        private const char VerticalDividerSymbol = '|';

        private const char EmptyCellPlaceholder = '_';
        private const char EmptyCellNumber = '0';

        public static Sudoku Parse(string path)
        {
            var rowList = ConvertPuzzleFileToRowList(path);
            var field = ConvertRowListToSudokuField(rowList);
            return new Sudoku(field);
        }

        private static int[,] ConvertRowListToSudokuField(List<int[]> rowList)
        {
            var dimension = rowList.Count;
            var boxDimension = (int) Math.Sqrt(dimension);

            if (boxDimension * boxDimension != dimension)
            {
                throw new InvalidDataException("Sudoku field dimension must be a square number.");
            }

            var sudokuField = new int[dimension, dimension];

            for (var row = 0; row < dimension; row++)
            for (var column = 0; column < dimension; column++)
            {
                var number = rowList[row][column];

                if (number >= 0 && number <= dimension)
                {
                    sudokuField[row, column] = rowList[row][column];
                }
                else
                {
                    throw new InvalidDataException($"Number {number} (row = {row + 1}, column = {column + 1}) not in range [0, {dimension}] " +
                                                   $"which is required in a {dimension}x{dimension} sudoku field.");
                }
            }

            return sudokuField;
        }

        private static List<int[]> ConvertPuzzleFileToRowList(string path)
        {
            var rowList = new List<int[]>();
            var requiredRowLength = -1;

            foreach (var line in File.ReadAllLines(path))
            {
                var trimmed = line.Trim();
                if (!IsNumberRow(trimmed)) continue;

                var row = ConvertLineToNumberArray(trimmed);

                if (requiredRowLength == -1)
                {
                    requiredRowLength = row.Length;
                }
                else if (row.Length != requiredRowLength)
                {
                    throw new InvalidDataException("Not all rows are of the same length.");
                }

                rowList.Add(row);
            }

            if (rowList.Count != requiredRowLength)
            {
                throw new InvalidDataException("Sudoku field does not have the same number of rows and columns.");
            }

            return rowList;
        }

        private static int[] ConvertLineToNumberArray(string line) => line
            .Replace(EmptyCellPlaceholder, EmptyCellNumber)
            .Replace(VerticalDividerSymbol, ' ')
            .Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        private static bool IsNumberRow(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return false;
            if (line.StartsWith(CommentSymbol)) return false;
            if (line.StartsWith(HorizontalDividerSymbol)) return false;
            return true;
        }
    }
}