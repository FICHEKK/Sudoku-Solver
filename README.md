![](Documentation/Header-2.png)

## What is it?
Sudoku solver, as the name suggests, is a tool designed to solve given sudoku puzzles.
The tool is capable of solving puzzles of any dimension, not just classic 9x9 sudoku puzzles.
The only constraint is that it is a valid and solvable sudoku puzzle. 


## How to use it?
Using the tool is a straightforward process; all you need to do is provide the tool with one or more sudoku definition files and the rest is done by the tool.

To achieve that, there are only 2 steps:

1. Define your sudoku puzzle(s).
2. Start sudoku solver and provide it with your sudoku puzzle(s).


## Defining a sudoku puzzle
Each sudoku puzzle should be defined in its own text file (1 puzzle = 1 text file).
The process is as simple as drawing a sudoku field using characters.

Let's take a look at a few examples to get the idea:

### 4x4 puzzle definition example `Puzzles/4x4/Easy-1.txt`:

```
# Field definition (_ defines empty slot):

_ 2 | 4 _
1 _ | _ 3
---------
4 _ | _ 2
_ 1 | 3 _
```

### 9x9 puzzle definition example `Puzzles/9x9/Hard-1.txt`:

```
# Field definition (_ defines empty slot):

_ _ _  |  _ _ 3  |  _ _ _
4 _ 8  |  _ _ _  |  _ _ _
_ _ _  |  8 _ 6  |  2 _ 3
-------------------------
3 _ _  |  _ 5 _  |  7 6 _
6 _ _  |  _ _ _  |  _ _ 4
_ 2 1  |  _ 3 _  |  _ _ 9
-------------------------
1 _ 9  |  3 _ 7  |  _ _ _
_ _ _  |  _ _ _  |  8 _ 7
_ _ _  |  1 _ _  |  _ _ _
```

### 16x16 puzzle definition example `Puzzles/16x16/Easy-2.txt`:

```
# Field definition (__ defines empty slot):

05 13 __ __ | __ 03 __ 12 | 08 11 10 02 | __ 09 01 __
__ 04 14 __ | __ 11 __ 01 | __ __ __ __ | 10 __ __ 15
__ __ 10 __ | __ __ 16 __ | __ __ __ 03 | 12 07 __ 08
01 __ 08 __ | __ 02 __ 10 | __ 06 __ 16 | 04 __ 03 13
-----------------------------------------------------
__ 09 12 __ | 01 __ 06 16 | __ __ __ __ | __ 13 __ 11
__ 14 __ __ | 12 07 __ __ | 02 __ 09 05 | 08 __ 15 04
__ __ __ 15 | __ __ 03 02 | 13 14 16 10 | __ 05 09 __
08 __ 02 __ | __ 05 13 14 | __ __ __ 11 | __ __ __ 16
-----------------------------------------------------
09 __ 06 __ | 15 10 02 08 | 07 __ 12 13 | 14 __ 16 05
__ 05 03 __ | __ 13 __ __ | 10 __ 08 __ | __ 15 12 01
__ 10 __ __ | __ 01 07 03 | 04 __ __ __ | 09 __ 13 __
02 08 16 __ | __ __ __ __ | 14 05 __ __ | __ 06 10 07
-----------------------------------------------------
03 __ __ 08 | __ 06 01 13 | 11 04 __ __ | __ 14 02 09
15 12 __ __ | 03 __ __ __ | __ 10 05 14 | __ 01 __ __
13 __ __ __ | 14 __ __ __ | 16 07 __ 01 | 05 __ __ 03
__ 07 11 01 | 02 12 05 __ | 06 __ __ 08 | 15 __ __ __
```

As you can see, defining a sudoku puzzle is a trivial task, however here are some notes:

* All of the empty lines or lines that start with character `#` will be ignored (`#` denotes a comment line). 

* `_` character defines an empty slot (slot that needs to be filled in). You can also put multiple `_` characters in a row and it will count as a single slot.

* Horizontal split chracter `-` and vertical split character `|` are optional, but they provide clarity and are recommended.

* File can have any extension (does not need to be `.txt`) as long as it contains a valid puzzle definition.

And that's all! You have successfully defined a sudoku puzzle. Now, let's solve it!


## Solving a sudoku puzzle
Once you have your sudoku puzzle definition file(s), all you need to do is start the solver and provide it path(s) of the definition file(s).

In general, this is the usage (from terminal): `SudokuSolver.exe path(s)`

Solving a single puzzle: `SudokuSolver.exe Puzzles/4x4/Easy-1.txt`

Solving multiple puzzles: `SudokuSolver.exe Puzzles/4x4/Easy-1.txt Puzzles/9x9/Hard-1.txt Puzzles/16x16/Easy-2.txt`


## Output
Finally, let's quickly analyse the output of the solver. Solving `Puzzles/9x9/Hard-1.txt` results in the following output:

```
================================================================================
                             Puzzles/9x9/Hard-1.txt
================================================================================

                       ----------------------------------
                       |          |        3 |          |
                       |  4     8 |          |          |
                       |          |  8     6 |  2     3 |
                       ----------------------------------
                       |  3       |     5    |  7  6    |
                       |  6       |          |        4 |
                       |     2  1 |     3    |        9 |
                       ----------------------------------
                       |  1     9 |  3     7 |          |
                       |          |          |  8     7 |
                       |          |  1       |          |
                       ----------------------------------

Solving puzzle 'Puzzles/9x9/Hard-1.txt'...
Done! Solution:

                       ----------------------------------
                       |  2  1  6 |  4  7  3 |  9  5  8 |
                       |  4  3  8 |  5  9  2 |  6  7  1 |
                       |  9  5  7 |  8  1  6 |  2  4  3 |
                       ----------------------------------
                       |  3  8  4 |  9  5  1 |  7  6  2 |
                       |  6  9  5 |  7  2  8 |  1  3  4 |
                       |  7  2  1 |  6  3  4 |  5  8  9 |
                       ----------------------------------
                       |  1  6  9 |  3  8  7 |  4  2  5 |
                       |  5  4  3 |  2  6  9 |  8  1  7 |
                       |  8  7  2 |  1  4  5 |  3  9  6 |
                       ----------------------------------

Puzzle 'Puzzles/9x9/Hard-1.txt' solved in 1,304ms.
Solution found at depth 57, in 2966 function calls.
Validity check: Passed!
```

Solver will keep track of the statistics, some of which you might find useful:
1. Solving time (in milliseconds) - can be used to compare the difficulties of sudoku puzzles.
2. Solution depth, which is the same as the number of empty slots in the initial sudoku definition.
3. Function call count - similarly to solving time, you can use this statistic for comparing puzzle difficulties.

Validity check is a final sanity check to make sure the solution does not break any rules. This test should always pass in a valid sudoku puzzle.



And that's everything! Have fun using the tool! 😃🧩
