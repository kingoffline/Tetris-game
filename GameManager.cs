using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Tetris_game
{
    internal class GameManager
    {
        private TetrisBoard _board;
        private ActivePiece _activePiece;
        private bool _gameOver;
        private int _score;
        private Random _random;
        private const int DropIntervall = 6000;
        private const int LoopDelay = 100;

        public GameManager()
        {
            _board = new TetrisBoard();
            _random = new Random();
            _gameOver = false;
            _score = 0;
        }

        public void StartGame()
        {
            _board.Clear();
            SpawnNewPiece();

            Stopwatch timer = new Stopwatch();
            timer.Start();
            long lastDropTime = timer.ElapsedMilliseconds;

            Console.CursorVisible = false;
            Console.Clear();

            while (!_gameOver)
            {
                HandleInput();

                long currentTime = timer.ElapsedMilliseconds;
                if (currentTime > lastDropTime)
                {
                    MovePieceDown();
                    lastDropTime = currentTime;
                }

                Render();

                Thread.Sleep(LoopDelay);
            }

        }

        private void SpawnNewPiece()
        {
            int randomShapeId = _random.Next(0, 5);
            _activePiece = new ActivePiece(randomShapeId);
            if (_board.CheckCollision(_activePiece, _activePiece.X, _activePiece.Y, _activePiece.RotationIndex))
            {
                _gameOver = true;
            }
        }

        private void HandleInput()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (!_board.CheckCollision(_activePiece, _activePiece.X - 1, _activePiece.Y, _activePiece.RotationIndex))
                            _activePiece.X--;
                        break;

                    case ConsoleKey.RightArrow:
                        if (!_board.CheckCollision(_activePiece, _activePiece.X + 1, _activePiece.Y, _activePiece.RotationIndex))
                            _activePiece.X++;
                        break;

                    case ConsoleKey.UpArrow:
                        int nextRotation = (_activePiece.RotationIndex + 1) % 4;
                        if (!_board.CheckCollision(_activePiece, _activePiece.X, _activePiece.Y, nextRotation))
                            _activePiece.Rotate();
                        break;

                    case ConsoleKey.DownArrow:
                        if (!_board.CheckCollision(_activePiece, _activePiece.X, _activePiece.Y + 1, _activePiece.RotationIndex))
                            _activePiece.Y++;
                        break;
                }
            }
        }

        private void MovePieceDown()
        {
            if (!_board.CheckCollision(_activePiece, _activePiece.X, _activePiece.Y + 1, _activePiece.RotationIndex))
            {
                _activePiece.Y++;
            }
            else
            {
                LockPieceToBoard();
                CheckLineClears();
                SpawnNewPiece();
            }
        }

        private void LockPieceToBoard()
        {
            int[,] matrix = _activePiece.GetCurrentMatrix();
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (matrix[r, c] != 0)
                    {
                        int boardX = _activePiece.X + c;
                        int boardY = _activePiece.Y + r;

                        if (boardY >= 0 && boardY < TetrisBoard.Height && boardX >= 0 && boardX < TetrisBoard.Width)
                        {
                            _board.Grid[boardY, boardX] = matrix[r, c];
                        }
                    }
                }
            }
        }

        private void CheckLineClears()
        {
            int linesClearedThisTurn = 0;

            for (int y = TetrisBoard.Height - 1; y >= 0; y--)
            {
                bool isLineComplete = true;

                for (int x = 0; x < TetrisBoard.Width; x++)
                {
                    if (_board.Grid[y, x] == 0)
                    {
                        isLineComplete = false;
                        break;
                    }
                }

                if (isLineComplete)
                {
                    linesClearedThisTurn++;

                    for (int moveY = y; moveY > 0; moveY--)
                    {
                        for (int x = 0; x < TetrisBoard.Width; x++)
                        {
                            _board.Grid[moveY, x] = _board.Grid[moveY - 1, x];
                        }
                    }

                    for (int x = 0; x < TetrisBoard.Width; x++)
                    {
                        _board.Grid[0, x] = 0;
                    }

                    y++;
                }
            }

            if (linesClearedThisTurn > 0)
            {
                _score += linesClearedThisTurn switch
                {
                    1 => 100,  
                    2 => 300,  
                    3 => 500,  
                    4 => 800, 
                    _ => 0
                };
            }
        }


        private void Render()
        {
            Console.SetCursorPosition(0, 0);

            int[,] pieceMatrix = _activePiece.GetCurrentMatrix();
            int pieceRows = pieceMatrix.GetLength(0);
            int pieceCols = pieceMatrix.GetLength(1);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("╔" + new string('═', TetrisBoard.Width * 2) + "╗");

            for (int y = 0; y < TetrisBoard.Height; y++)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("║");

                for (int x = 0; x < TetrisBoard.Width; x++)
                {
                    int blockValue = _board.Grid[y, x];

                    int pieceCol = x - _activePiece.X;
                    int pieceRow = y - _activePiece.Y;

                    if (pieceRow >= 0 && pieceRow < pieceRows && pieceCol >= 0 && pieceCol < pieceCols)
                    {
                        if (pieceMatrix[pieceRow, pieceCol] != 0)
                        {
                            blockValue = pieceMatrix[pieceRow, pieceCol];
                        }
                    }

                    if (blockValue == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("  ");
                    }
                    else
                    {
                        Console.ForegroundColor = TetrominoData.GetColor(blockValue);
                        Console.Write("██");
                    }
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("║");
            }

            Console.WriteLine("╚" + new string('═', TetrisBoard.Width * 2) + "╝");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n Score: {_score}");
        }



    }
}
