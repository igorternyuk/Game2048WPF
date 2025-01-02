using Game2048.Infrastructure.Commands;
using Game2048.Models;
using Game2048.ViewModels.Base;
using Microsoft.VisualBasic;
using System.Windows;
using System.Windows.Input;

namespace Game2048.ViewModels
{
    internal class GameViewModel : ViewModel
    {
        #region Fields

        private const string winMessage = "You won! Would you like to continue?";
        private const string lossMessage = "You lost! Would you like to save score to statistics?";
        private const string playAgainMessage = "Would you like to play again?";
        private const string gameOverMessage = "Game over!";
        private const string congratulationsTitle = "Congratulations!";
        private const string youLostTitle = "You lost!";
        private const string playAgainTitle = "Play again?";
        
        private readonly Game _game;
        private int[,] _boardGrid;
        private int _score;
        #endregion

        #region Properties

        public int[,] BoardGrid { get => _boardGrid; set=> Set(ref _boardGrid, value); }

        public int Score { get => _score; set => Set(ref _score, value); }
        #endregion

        #region Contructors
        public GameViewModel()
        {
            _game = new Game();
            BoardGrid = new int[_game.BoardSize, _game.BoardSize];
            Score = _game.Board.Score;
            ShutdownCommand = new LambdaCommand(OnShutdownCommandExecuted, CanShutdownCommandExecute);
            SlideLeftCommand = new LambdaCommand(OnSlideLeftCommandExecuted, CanSlideLeftCommandExecute);
            SlideRightCommand = new LambdaCommand(OnSlideRightCommandExecuted, CanSlideRightCommandExecute);
            SlideUpCommand = new LambdaCommand(OnSlideUpCommandExecuted, CanSlideUpCommandExecute);
            SlideDownCommand = new LambdaCommand(OnSlideDownCommandExecuted, CanSlideDownCommandExecute);
            NewGameCommand = new LambdaCommand(OnNewGameCommandExecuted, CanNewGameCommandExecute);
            Reset();
        }
        #endregion

        #region Commands

        public ICommand NewGameCommand { get; }

        private bool CanNewGameCommandExecute(object p) => _game.IsPlaying();

        private void OnNewGameCommandExecuted(object p)
        {
            _game.Reset();
            Update();
        }

        public NavigationCommand NavigateToMenuPageCommand { get => new(NavigateToPage, new Uri("Views/Pages/MainMenuPage.xaml", UriKind.RelativeOrAbsolute)); }

        public ICommand SlideLeftCommand { get; }

        private bool CanSlideLeftCommandExecute(object p) => _game.IsPlaying();

        private void OnSlideLeftCommandExecuted(object p)
        {
            SlideLeft();
        }
        public ICommand SlideRightCommand { get; }

        private bool CanSlideRightCommandExecute(object p) => _game.IsPlaying();

        private void OnSlideRightCommandExecuted(object p)
        {
            SlideRight();
        }

        public ICommand SlideUpCommand { get; }

        private bool CanSlideUpCommandExecute(object p) => _game.IsPlaying();

        private void OnSlideUpCommandExecuted(object p)
        {
            SlideUp();
        }

        public ICommand SlideDownCommand { get; }

        private bool CanSlideDownCommandExecute(object p) => _game.IsPlaying();

        private void OnSlideDownCommandExecuted(object p)
        {
            SlideDown();
        }

        public ICommand ResetCommand { get; }

        private bool CanResetCommandExecuted(object p) => true;

        private void OnResetCommandExecuted(object p)
        {
            Reset();
        }

        public ICommand ShutdownCommand { get; }

        private bool CanShutdownCommandExecute(object p) => true;

        private void OnShutdownCommandExecuted(object p)
        {
            Quit();
        }

        #endregion

        #region Methods
        private void Reset()
        {
            _game.Reset();
            Update();
        }

        private void Update()
        {
            UpdateBoardGrid();
            UpdateScore();
            CheckGameStatus();
        }

        private void UpdateScore()
        {
            Score = _game.Score;
        }

        private void UpdateBoardGrid()
        {
            bool needToUpdateBoardGrid = false;
            for (int r = 0; r < BoardGrid.GetLength(0); ++r)
            {
                for (int c = 0; c < BoardGrid.GetLength(1); ++c)
                {
                    if (!BoardGrid[r, c].Equals(_game.Board.Grid[r, c]))
                    {
                        BoardGrid[r, c] = _game.Board.Grid[r, c];
                        needToUpdateBoardGrid = true;
                    }

                }
            }
            if (needToUpdateBoardGrid)
                OnPropertyChanged(nameof(BoardGrid));
        }

        private void CheckGameStatus()
        {
            if(_game.IsLoss())
            {
                MessageBoxResult result =  MessageBox.Show(lossMessage, youLostTitle, MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result == MessageBoxResult.Yes)
                {
                    SaveGameResultsToStatistics();
                }

                result = MessageBox.Show(playAgainMessage, playAgainTitle, MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    Reset();
                }
                else
                {
                    OnShutdownCommandExecuted(null);
                }
            }
            else if(_game.IsWinScoreReached())
            {
                MessageBoxResult  result = MessageBox.Show(winMessage, congratulationsTitle, MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result != MessageBoxResult.Yes)
                {
                    OnShutdownCommandExecuted(null);
                }
            }
        }

        public void SlideLeft()
        {
            _game.SlideLeft();
            Update();
        }

        public void SlideRight()
        {
            _game.SlideRight();
            Update();
        }

        public void SlideUp()
        {
            _game.SlideUp();
            Update();
        }

        public void SlideDown()
        {
            _game.SlideDown();
            Update();
        }

        private void SaveGameResultsToStatistics()
        {
            string name = "";
            int iter = 0;
            do
            {
                ++iter;
                name = Microsoft.VisualBasic.Interaction.InputBox("Input your name", "Name input", "");
            } while(string.IsNullOrEmpty(name) && iter < 100);
            if(string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name could not be empty! Try again please.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                name = "Noname";
            }
            //Statistics.Add(name, Score.ToString())
        }

        #endregion Methods
    }
}
