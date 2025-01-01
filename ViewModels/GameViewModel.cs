using Game2048.Infrastructure.Commands;
using Game2048.Models;
using Game2048.ViewModels.Base;
using System.Windows.Input;

namespace Game2048.ViewModels
{
    internal class GameViewModel : ViewModel
    {
        #region Fields
        
        private readonly Game _game;
        private int[,] _boardGrid;
        private int _score;
        #endregion

        #region Properties

        public int[,] BoardGrid { get => _boardGrid; set=> Set(ref _boardGrid, value); }

        public int Score { get => _score; set => Set(ref _score, value); 
        }
        #endregion

        #region Contructors
        public GameViewModel()
        {
            _game = new Game();
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
            Reset();
        }
        private void Reset()
        {
            _game.Reset();
            Update();
        }

        private void Update()
        {
            BoardGrid = _game.Board.Grid;
            Score = _game.Score;
        }

        private void CheckGameStatus()
        {
            Game.GameStatus status =  _game.Status;
        }

        public void SlideLeft()
        {
            _game.SlideLeft();
        }

        public void SlideRight()
        {
            _game.SlideRight();
        }

        public void SlideUp()
        {
            _game.SlideUp();
        }

        public void SlideDown()
        {
            _game.SlideUp();
        }

        #endregion Methods
    }
}
