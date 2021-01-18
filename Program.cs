using System;

namespace SudokuSolver
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("Program requires one or more sudoku definition file paths.");

            foreach (var sudokuDefinitionPath in args)
            {
                SolveSudokuAndDisplayResults(sudokuDefinitionPath);
            }
        }

        private static void SolveSudokuAndDisplayResults(string sudokuDefinitionPath)
        {
            var initialState = SudokuParser.Parse(sudokuDefinitionPath);

            Console.WriteLine($"Solving puzzle '{sudokuDefinitionPath}':");
            PrintSudokuField(initialState);

            var (solution, functionCallCount, elapsedTimeInMs) = SudokuSolver.Solve(initialState);

            if (solution != null)
            {
                Console.WriteLine("Done! Solution:");
                PrintSudokuField(solution);

                Console.WriteLine($"Puzzle '{sudokuDefinitionPath}' solved in {elapsedTimeInMs:F3}ms.");
                Console.WriteLine($"Solution found at depth {initialState.MissingNumbersCount}, in {functionCallCount} function calls.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Puzzle could not be solved. Make sure the puzzle was defined properly.");
            }
        }

        private static void PrintSudokuField(SudokuState state)
        {
            Console.WriteLine();
            Console.WriteLine(state);
            Console.WriteLine();
        }
    }
}