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

        public static SudokuState Parse(string path)
        {
            var rowList = ConvertPuzzleFileToRowList(path);
            var field = ConvertRowListToSudokuField(rowList);
            return new SudokuState(field);
        }

        private static int[,] ConvertRowListToSudokuField(List<int[]> rowList)
        {
            var dimension = rowList.Count;
            var sudokuField = new int[dimension, dimension];

            for (var row = 0; row < dimension; row++)
            {
                for (var column = 0; column < dimension; column++)
                {
                    sudokuField[row, column] = rowList[row][column];
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
                    throw new InvalidDataException("All of the rows must be of the same length.");
                }

                rowList.Add(row);
            }

            if (rowList.Count != requiredRowLength)
            {
                throw new InvalidDataException("Field definition must be a square matrix.");
            }

            return rowList;
        }

        private static int[] ConvertLineToNumberArray(string line) => line
            .Replace('_', '0')
            .Replace('|', ' ')
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