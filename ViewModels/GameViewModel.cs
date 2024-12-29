using Game2048.Infrastructure.Commands;
using Game2048.Models;
using Game2048.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game2048.ViewModels
{
    internal class GameViewModel : ViewModel
    {
        #region Fields
        
        private GameBoard _gameBoard;
        private Random _random;
        private readonly int _probabilityOfFour = 90;
        private readonly int maxIter = 5000;

        #endregion

        #region Properties
        public int[,] Board { get => _gameBoard.Board; set=> Set(ref _gameBoard.board, value); }

        public int Score {  get => _gameBoard.Score; set => Set(ref _gameBoard.score, value); }
        #endregion

        #region Contructors
        public GameViewModel()
        {
            Init();
        }
        #endregion

        #region Commands
        public NavigationCommand NavigateToMenuPageCommand { get => new(NavigateToPage, new Uri("Views/Pages/MainMenuPage.xaml", UriKind.RelativeOrAbsolute)); }
        public ICommand ResetCommand { get; }

        private bool CanResetCommandExecuted(object p) => true;

        private void OnResetCommandExecuted(object p)
        {
            Reset();
        }

        #endregion

        #region Methods

        private void Init()
        {
            _gameBoard = new GameBoard();
            _random = new Random();
            Reset();
        }
        private void Reset()
        {
            Board = new int[_gameBoard.BoardSize, _gameBoard.BoardSize];
            SetTileOnRandomPosition();
            SetTileOnRandomPosition();
            Update();
        }

        private void SetTileOnRandomPosition()
        {
            int row, col;
            
            int iter = 0;
            do
            {
                row = _random.Next(_gameBoard.BoardSize);
                col = _random.Next(_gameBoard.BoardSize);
            } while (_gameBoard.board[row, col] != 0 && iter++ < maxIter);

            _gameBoard.board[row, col] = GenerateRandomTileValue();
        }

        private int GenerateRandomTileValue()
        {
            return _random.Next(100) < _probabilityOfFour ? 2 : 4;
        }

        private void Update()
        {
            Board = _gameBoard.board;
            Score = _gameBoard.score;
        }

        #endregion Methods
    }
}
