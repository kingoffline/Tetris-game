using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris_game
{
    internal class ActivePiece
    {
        public int ShapeID { get; set; }
        public int RotationIndex { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public ActivePiece(int shapeID)
        {
            ShapeID = shapeID;
            RotationIndex = 0;
            X = 3;
            Y = 0;
        }

        public int[,] GetCurrentMatrix()
        {
            int[,,] allRotations = TetrominoData.Shapes[ShapeID];
            int rows = allRotations.GetLength(1);
            int cols = allRotations.GetLength(2);

            int[,] currentMatrix = new int[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    currentMatrix[r,c] = allRotations[RotationIndex,r,c];
                }
            }

            return currentMatrix;
        }

        public void Rotate()
        {
            RotationIndex = (RotationIndex + 1) % 4;
        }
    }
}