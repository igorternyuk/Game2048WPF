using Game2048.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Game2048.Models
{
    internal class Game
    {
        #region Fields
        
        public const int BoardSizeDefault = 4;
        public readonly int BoardSize;
        public const int WinValue = 2048;
        
        private const int _probabilityOfFour = 90;
        private const int maxIter = 5000;
        private Random _random;
        
        #endregion

        public enum GameStatus
        {
            PLAY,
            REACHED2048,
            LOSS
        }

        #region Properties
        public GameStatus Status { get; set; }

        public Board Board { get; set; }

        public int Score { get; set; }
        #endregion

        #region Constructors
        public Game(int boardSize = BoardSizeDefault)
        {
            BoardSize = boardSize;
            _random = new Random();
            Board = new Board(BoardSize);
            Reset();
        }
        #endregion

        #region Methods

        public bool IsPlaying()
        {
            if (!IsLoss() && !IsOver())
                return true;
            return false;
        }

        public bool IsLoss()
        {
            if (Status == GameStatus.LOSS)
                return true;
            return false;
        }

        public bool IsWinScoreReached()
        {
            if (Status == GameStatus.REACHED2048)
                return true;
            return false;
        }

        public bool IsOver()
        {
            if (IsLoss() && IsWinScoreReached())
                return true;
            return false;
        }

        public void Reset()
        {
            Status = GameStatus.PLAY;
            Board.Reset();
            SetTileOnRandomPosition();
            SetTileOnRandomPosition();
        }

        private void SetTileOnRandomPosition()
        {
            int row, col;
            int iter = 0;
            do
            {
                row = _random.Next(Board.Size);
                col = _random.Next(Board.Size);
            } while (Board.Grid[row, col] != 0 && iter++ < maxIter);
            int randomValue = GenerateRandomTileValue();
            Board.Grid[row, col] = randomValue;
        }

        private int GenerateRandomTileValue()
        {
            return _random.Next(100) < _probabilityOfFour ? 2 : 4;
        }

        private void Update()
        {
            UpdateScore();
            UpdateStatus();
            if (IsPlaying())
                SetTileOnRandomPosition();
            UpdateStatus();
        }

        private void UpdateScore()
        {
            Score = Board.Score;
        }

        private void UpdateStatus()
        {
            if (!Board.CanSlide())
                Status = GameStatus.LOSS;
            else if (Score < WinValue)
                Status = GameStatus.PLAY;
            else
                Status = GameStatus.REACHED2048;
        }

        public void SlideLeft()
        {
            if (IsOver()) 
                return;
            Board.SlideLeft();
            Update();
        }

        public void SlideRight()
        {
            if (IsOver())
                return;
            Board.SlideRight();
            Update();
        }

        public void SlideUp()
        {
            if (IsOver())
                return;
            Board.SlideUp();
            Update();
        }

        public void SlideDown()
        {
            if (IsOver())
                return;
            Board.SlideDown();
            Update();
        }
        #endregion
    }
}
