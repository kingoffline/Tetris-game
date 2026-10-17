# Tetris game
a retro, text-mode Tetris game in a C# Console Application

## Game State & Data Structures
- TetrisBoard Class: Represent 10×30 playing field using a two-dimensional array int[10, 30]. Empty cells can be designated as 0, while locked blocks can store numbers corresponding to distinct colors or characters (e.g., 1 for Cyan, 2 for Blue).
- TetrominoData Class: Model each of the 5 classic shapes (I,O,T,L,S) inside a 3×3 or 4×4 boundary matrix. The shapes's rotations are managed by hardcoding an array of 4 pre-calculated rotational orientations for every shape.
- ActivePiece Class: Keep track of the active piece separately from the background matrix. It needs properties tracking its current shape matrix, its X and Y grid coordinates, and its current rotation index.
- GameManager Class: Is the core of the game, It handles Spawning new piece, User input, Locking piece to the board, Check if it is complete line, Score and Rendering.