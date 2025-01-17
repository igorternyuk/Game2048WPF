﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Game2048.Models
{
    internal class Board
    {
        #region Fields
        public readonly int[,] grid;
        private int _score = 0;
        private int _currentMaxValue = 0;
        #endregion

        #region Properties
        public int Size { get; }
        public int[,] Grid { get => grid; }
        public int Score { get => _score; private set => _score = value; }

        public int CurrentMaxTileValue { get => _currentMaxValue; set => _currentMaxValue = value; }
        #endregion

        #region Constructors
        public Board(int size)
        {
            Size = size;
            grid = new int[size, size];
            Reset();
        }
        #endregion

        #region Methods
        public void Reset()
        {
            Score = 0;
            CurrentMaxTileValue = 0;

            for (int r = 0; r < grid.GetLength(0); ++r)
            {
                for (int c = 0; c < grid.GetLength(1); ++c)
                {
                    grid[r, c] = 0;
                }
            }
        }

        public bool IsFull()
        {
            for (int r = 0; r < grid.GetLength(0); ++r)
            {
                for (int c = 0; c < grid.GetLength(1); ++c)
                {
                    if (grid[r, c] == 0)
                        return false;
                }
            }
            return true;
        }

        public bool CanSlide()
        {
            for (int r = 0; r < grid.GetLength(0); ++r)
            {
                for (int c = 0; c < grid.GetLength(1); ++c)
                {
                    if (grid[r, c] == 0)
                        return true;
                    if (c > 0 && grid[r, c] == grid[r, c - 1])
                        return true;
                    if (c < grid.GetLength(1) - 1 && grid[r, c] == grid[r, c + 1])
                        return true;
                    if (r > 0 && grid[r, c] == grid[r - 1, c])
                        return true;
                    if (r < grid.GetLength(1) - 1 && grid[r, c] == grid[r + 1, c])
                        return true;
                }
            }
            return false;
        }

        public void SlideLeft()
        {
            for (int r = 0; r < grid.GetLength(0); ++r)
            {
                CombineTilesInRow(r);
                SlideTilesLeft(r);
            }
        }

        private void CombineTilesInRow(int r)
        {
            for (int c = 0; c < grid.GetLength(1); ++c)
            {
                if (grid[r, c] == 0)
                    continue;

                int index = c + 1;
                while (index < grid.GetLength(1) && grid[r, index] == 0)
                    ++index;

                if (index < grid.GetLength(1) && grid[r, index] == grid[r, c])
                {
                    grid[r, c] *= 2;
                    grid[r, index] = 0;
                    Score += grid[r, c];
                    if(CurrentMaxTileValue < grid[r, c])
                    {
                        CurrentMaxTileValue = grid[r, c];
                    }
                }
            }
        }

        private void SlideTilesLeft(int r)
        {
            for (int c = 0; c < grid.GetLength(1); ++c)
            {
                int index = c;

                while (index < grid.GetLength(1) && grid[r, index] == 0)
                    ++index;

                if (index >= grid.GetLength(1))
                    break;

                if (c != index)
                {
                    grid[r, c] = grid[r, index];
                    grid[r, index] = 0;
                }
            }
        }
        
        public void SlideRight()
        {
            RotateCounterClockwise();
            RotateCounterClockwise();
            SlideLeft();
            RotateClockwise();
            RotateClockwise();
        }

        public void SlideUp()
        {
            RotateCounterClockwise();
            SlideLeft();
            RotateClockwise();
        }

        public void SlideDown()
        {
            RotateClockwise();
            SlideLeft();
            RotateCounterClockwise();
        }

        private void RotateCounterClockwise()
        {
            Transpose();
            InverseColumns();
        }

        private void RotateClockwise()
        {
            Transpose();
            InverseRows();
        }
        private void InverseRows()
        {
            var len1 = grid.GetLength(1);
            for (int r = 0; r < grid.GetLength(0); r++)
            {
                for (int c = 0; c < len1 / 2; c++)
                {
                    int buf = grid[r, c];
                    grid[r, c] = grid[r, len1 - 1 - c];
                    grid[r, len1 - 1 - c] = buf;
                }
            }
        }

        private void InverseColumns()
        {
            var len0 = grid.GetLength(0);
            for (int r = 0; r < len0 / 2; r++)
            {
                for (int c = 0; c < grid.GetLength(1); c++)
                {
                    int buf = grid[r, c];
                    grid[r, c] = grid[len0 - 1 - r, c];
                    grid[len0 - 1 - r, c] = buf;
                }
            }
        }

        private void Transpose()
        {
            for(int r = 1; r < grid.GetLength(0); ++r)
            {
                for(int c = 0; c < r; c++)
                {
                    (grid[r, c], grid[c, r]) = (grid[c, r], grid[r, c]);
                }
            }
        }

        #endregion
    }
}
