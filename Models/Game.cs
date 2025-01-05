using Game2048.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

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
            Play,
            Reached2048,
            Loss,
            GameOver,
        }

        #region Properties
        public GameStatus Status { get; set; }

        public Board Board { get; set; }

        public int Score { get; set; }

        public int CurrentMaxTileValue { get; set;  }

        public bool ContinuePlayingAfter2048 { get; set; }
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
            if (Status == GameStatus.Play)
                return true;
            return false;
        }

        public bool IsLoss()
        {
            if (Status == GameStatus.Loss)
                return true;
            return false;
        }

        public bool IsWinScoreReached()
        {
            if (Status == GameStatus.Reached2048)
                return true;
            return false;
        }

        public bool IsOver()
        {
            if (Status == GameStatus.GameOver)
                return true;
            return false;
        }

        public void Reset()
        {
            Status = GameStatus.Play;
            Score = 0;
            ContinuePlayingAfter2048 = false;
            Board.Reset();
            SetTileOnRandomPosition();
            SetTileOnRandomPosition();
            CurrentMaxTileValue = Board.CurrentMaxTileValue;
        }

        public void SetTileOnRandomPosition()
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
            Board.CurrentMaxTileValue = Math.Max(randomValue, Board.CurrentMaxTileValue);
        }

        private int GenerateRandomTileValue()
        {
            return _random.Next(100) < _probabilityOfFour ? 2 : 4;
        }

        private void Update()
        {
            UpdateScore();
            UpdateCurrentMaxTileValue();
            UpdateStatus();
            if (IsPlaying() && !Board.IsFull())
                SetTileOnRandomPosition();
            UpdateStatus();
        }

        private void UpdateScore()
        {
            Score = Board.Score;
        }
        private void UpdateCurrentMaxTileValue()
        {
            CurrentMaxTileValue = Board.CurrentMaxTileValue;
        }

        private void UpdateStatus()
        {
            if (!Board.CanSlide())
            {
                Status = CurrentMaxTileValue < WinValue ? GameStatus.Loss : GameStatus.GameOver;
            }
            else if (CurrentMaxTileValue < WinValue)
                Status = GameStatus.Play;
            else
                Status = ContinuePlayingAfter2048 ? GameStatus.Play : GameStatus.Reached2048;
        }

        public void SlideLeft()
        {
            if (!IsPlaying())
                return;
            Board.SlideLeft();
            Update();
        }

        public void SlideRight()
        {
            if (!IsPlaying())
                return;
            Board.SlideRight();
            Update();
        }

        public void SlideUp()
        {
            if (!IsPlaying())
                return;
            Board.SlideUp();
            Update();
        }

        public void SlideDown()
        {
            if (!IsPlaying())
                return;
            Board.SlideDown();
            Update();
        }
        #endregion
    }
}
