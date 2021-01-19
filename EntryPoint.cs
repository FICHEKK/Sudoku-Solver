using System;

namespace SudokuSolver
{
    public static class EntryPoint
    {
        private const int HeaderLineLength = 80;

        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine("Solver requires one or more sudoku definition file paths.");
                return;
            }

            foreach (var sudokuPath in args)
            {
                SolveSudokuAndDisplayResults(sudokuPath);
            }
        }

        private static void SolveSudokuAndDisplayResults(string sudokuPath)
        {
            var initial = SudokuParser.Parse(sudokuPath);
            var maxDepth = SudokuInspector.CountEmptyCells(initial);

            PrintHeader(sudokuPath);
            PrintSudoku(initial);
            Console.WriteLine($"Solving puzzle '{sudokuPath}'...");

            var (solution, functionCallCount, elapsedTimeInMs) = SudokuSolver.Solve(initial);

            if (solution != null)
            {
                Console.WriteLine("Done! Solution:");
                PrintSudoku(solution);

                Console.WriteLine($"Puzzle '{sudokuPath}' solved in {elapsedTimeInMs:F3}ms.");
                Console.WriteLine($"Solution found at depth {maxDepth}, in {functionCallCount} function calls.");
                Console.WriteLine($"Validity check: {(SudokuValidator.Validate(solution) ? "Passed!" : "Failed.")}");
            }
            else
            {
                Console.WriteLine("Puzzle could not be solved. Please make sure the puzzle was defined properly.");
            }

            Console.WriteLine();
        }

        private static void PrintHeader(string title)
        {
            var line = new string('=', HeaderLineLength);
            var indentation = new string(' ', (HeaderLineLength - title.Length) / 2);

            Console.WriteLine(line);
            Console.WriteLine($"{indentation}{title}");
            Console.WriteLine(line);
        }

        private static void PrintSudoku(Sudoku sudoku)
        {
            Console.WriteLine();
            Console.WriteLine(SudokuStringifier.Stringify(sudoku, HeaderLineLength));
            Console.WriteLine();
        }
    }
}