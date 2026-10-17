using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris_game
{
    public static class TetrominoData
    {
        // A 4D array: [ShapeType 0-6][Rotation 0-3][Row][Column]
        public static readonly int[][,,] Shapes = new int[][,,]
        {
        // 0: I-Piece (4x4 matrix to allow clean rotation centering)
        new int[4, 4, 4] {
            {
                {0,0,0,0},
                {1,1,1,1},
                {0,0,0,0},
                {0,0,0,0}
            },
            {
                {0,0,1,0},
                {0,0,1,0},
                {0,0,1,0},
                {0,0,1,0}
            },
            {
                {0,0,0,0},
                {0,0,0,0},
                {1,1,1,1},
                {0,0,0,0}
            },
            {
                {0,1,0,0},
                {0,1,0,0},
                {0,1,0,0},
                {0,1,0,0}
            }
        },
        // 1: O-Piece (2x2 matrix - doesn't change when rotated)
        new int[4, 2, 2] {
            {
                {2,2},
                {2,2}
            },
            {
                {2,2},
                {2,2}
            },
            {
                {2,2},
                {2,2}
            },
            {
                {2,2},
                {2,2}
            }
        },
        // 2: T-Piece (3x3 matrix)
        new int[4, 3, 3] {
            {
              {0,3,0},
              {3,3,3},
              {0,0,0}
            },
            {
              {0,3,0},
              {0,3,3},
              {0,3,0}
            },
            {
              {0,0,0},
              {3,3,3},
              {0,3,0}
            },
            {
              {0,3,0},
              {3,3,0},
              {0,3,0}
            }
        },
            // 3: L-Piece
        new int [4, 3,3] {
            { {0,0,3},
              {3,3,3},
              {0,0,0}
            },
            { {0,3,0},
              {0,3,0},
              {0,3,3}
            },
            {
              {0,0,0},
              {3,3,3},
              {3,0,0}
            },
            { {3,3,0},
              {0,3,0},
              {0,3,0}
            },
        },
        // 4: S-Piece
        new int [4,3,3] {
        {     {0,3,3},
              {3,3,0},
              {0,0,0}
            },
            { {0,3,0},
              {0,3,3},
              {0,0,3}
            },
            {
              {0,0,0},
              {0,3,3},
              {3,3,0}
            },
            { {3,0,0},
              {3,3,0},
              {0,3,0}
            },
        }
        };

        public static ConsoleColor GetColor(int shapeID)
        {
            return shapeID switch
            {
                1 => ConsoleColor.Green,    // 0: I-Piece
                2 => ConsoleColor.Red,      // 1: O-Piece
                3 => ConsoleColor.Yellow,   // 2: T-Piece
                4 => ConsoleColor.Blue,     // 3: L-Piece
                5 => ConsoleColor.Cyan,     // 4: S-Piece
                _ => ConsoleColor.Black,
            };
        }
    }
}