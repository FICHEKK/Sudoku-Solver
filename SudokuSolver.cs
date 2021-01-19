using System.Diagnostics;

namespace SudokuSolver
{
    public static class SudokuSolver
    {
        public static (Sudoku solution, int functionCallCount, double elapsedTimeInMs) Solve(Sudoku initial)
        {
            if (!SudokuValidator.Validate(initial)) return (null, 0, 0);

            var stopwatch = Stopwatch.StartNew();
            var functionCallCount = 0;
            var solution = Solve(initial, ref functionCallCount);

            return (solution, functionCallCount, stopwatch.Elapsed.TotalMilliseconds);
        }

        private static Sudoku Solve(Sudoku sudoku, ref int functionCallCount)
        {
            functionCallCount++;

            var (row, column) = SudokuInspector.FindFirstEmptyPosition(sudoku);
            if (row == -1) return sudoku;

            for (var number = 1; number <= sudoku.FieldDimension; number++)
            {
                if (!sudoku.IsNumberValid(row, column, number)) continue;

                sudoku.SetCell(row, column, number);
                var solution = Solve(sudoku, ref functionCallCount);

                if (solution != null) return solution;
                sudoku.ResetCell(row, column);
            }

            return null;
        }
    }
}