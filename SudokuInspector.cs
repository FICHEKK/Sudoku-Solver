namespace SudokuSolver
{
    public static class SudokuInspector
    {
        public static int CountEmptyCells(Sudoku sudoku)
        {
            var emptyCellsCount = 0;

            for (var row = 0; row < sudoku.FieldDimension; row++)
            for (var column = 0; column < sudoku.FieldDimension; column++)
            {
                if (sudoku[row, column] == Sudoku.EmptyCell) emptyCellsCount++;
            }

            return emptyCellsCount;
        }

        public static (int row, int column) FindFirstEmptyPosition(Sudoku sudoku)
        {
            for (var row = 0; row < sudoku.FieldDimension; row++)
            for (var column = 0; column < sudoku.FieldDimension; column++)
            {
                if (sudoku[row, column] == Sudoku.EmptyCell) return (row, column);
            }

            return (-1, -1);
        }
    }
}