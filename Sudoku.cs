using System;

namespace SudokuSolver
{
    public class Sudoku
    {
        public const int EmptyCell = 0;

        private readonly int[,] _field;
        private readonly int[,,] _cellsNumberCounters;

        public int FieldDimension { get; }
        public int BoxDimension { get; }

        public Sudoku(int[,] field)
        {
            if (field.GetLength(0) != field.GetLength(1))
                throw new ArgumentException("Could not create sudoku: Row dimension does not match column dimension.");

            FieldDimension = field.GetLength(0);
            BoxDimension = (int) Math.Sqrt(FieldDimension);

            if (BoxDimension * BoxDimension != FieldDimension)
                throw new ArgumentException("Sudoku field dimension must be a square number.");

            // Number of counting variables for each cell is 'field dimension + 1'.
            // Reason is simple: index 0 variable is ignored to achieve natural mapping.
            // Without additional slot, counter for number N would be at index N - 1
            // which would require mapping and add slight overhead.
            // For example, counter for number 1 is at index 1, whereas without the
            // additional slot it would be at index 0.
            _cellsNumberCounters = new int[FieldDimension, FieldDimension, FieldDimension + 1];
            _field = field;

            AddInitialClues();
        }

        private void AddInitialClues()
        {
            for (var row = 0; row < FieldDimension; row++)
            for (var column = 0; column < FieldDimension; column++)
            {
                if (_field[row, column] == EmptyCell) continue;
                SetCell(row, column, _field[row, column]);
            }
        }

        public int this[int row, int column] => _field[row, column];

        public bool IsNumberValid(int row, int column, int number) =>
            _cellsNumberCounters[row, column, number] == 0;

        public void SetCell(int row, int column, int number)
        {
            UpdateCellCounters(row, column, number, +1);
            _field[row, column] = number;
        }

        public void ResetCell(int row, int column)
        {
            UpdateCellCounters(row, column, _field[row, column], -1);
            _field[row, column] = EmptyCell;
        }

        private void UpdateCellCounters(int row, int column, int counterIndex, int counterDelta)
        {
            for (var i = 0; i < FieldDimension; i++)
                _cellsNumberCounters[row, i, counterIndex] += counterDelta;

            for (var i = 0; i < FieldDimension; i++)
                _cellsNumberCounters[i, column, counterIndex] += counterDelta;

            var boxRow = row / BoxDimension;
            var boxColumn = column / BoxDimension;

            // Cells which are both in the box and in the row/column will have
            // their counter incremented twice. This, however, does not change
            // the outcome of the algorithm as removing from the same cell
            // will decrement same cells twice and completely undo the operation.
            for (var rowOffset = 0; rowOffset < BoxDimension; rowOffset++)
            for (var columnOffset = 0; columnOffset < BoxDimension; columnOffset++)
            {
                var r = boxRow * BoxDimension + rowOffset;
                var c = boxColumn * BoxDimension + columnOffset;
                _cellsNumberCounters[r, c, counterIndex] += counterDelta;
            }
        }
    }
}