using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris_game
{
    internal class TetrisBoard
    {
        public const int Width = 10;
        public const int Height = 30;
        public int[,] Grid { get; private set; } = new int[Height, Width];


        public void Clear()
        {
            Array.Clear(Grid, 0, Grid.Length);
        }

        public bool CheckCollision(ActivePiece piece, int targetX, int targetY, int targetRotation)
        {
            int[,,] allRotation = TetrominoData.Shapes[piece.ShapeID];
            int rows = allRotation.GetLength(1);
            int cols = allRotation.GetLength(2);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    // if these grids are filled, !=0
                    if(allRotation[targetRotation, r, c] != 0)
                    {
                        int boardX = targetX + c;
                        int boardY = targetY + r;

                        // Collided with the left or right walls or the floor
                        if(boardX < 0 || boardX >= Width || boardY >= Height)
                        {
                            return true;
                        }

                        //
                        if (boardY >= 0)
                        {
                            if (Grid[boardY, boardX]  != 0)
                            {
                                return true;
                            }
                        }
                    }

                }
            }

            return false;
        }
    }

}
