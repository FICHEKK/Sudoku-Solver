using System.Diagnostics;

namespace SudokuSolver
{
    public static class SudokuSolver
    {
        public static (SudokuState solution, int functionCallCount, double elapsedTimeInMs) Solve(SudokuState initialState)
        {
            if (!initialState.IsValid) return (null, 0, 0);

            var stopwatch = Stopwatch.StartNew();
            var functionCallCount = 0;
            var solution = Solve(initialState, ref functionCallCount);

            return (solution, functionCallCount, stopwatch.Elapsed.TotalMilliseconds);
        }

        private static SudokuState Solve(SudokuState state, ref int functionCallCount)
        {
            functionCallCount++;

            var (row, column) = state.FirstEmptyPosition;
            if (row == -1) return state;

            for (var number = 1; number <= state.FieldDimension; number++)
            {
                var isInputValid = state.CheckNumber(row, column, number);
                if (!isInputValid) continue;

                var stateWithNumber = state.PutNumber(row, column, number);
                var solution = Solve(stateWithNumber, ref functionCallCount);

                if (solution != null) return solution;
            }

            return null;
        }
    }
}