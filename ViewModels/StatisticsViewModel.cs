using Game2048.Infrastructure.Commands;
using Game2048.ViewModels.Base;
using Game2048.Models;
using Game2048.Infrastructure.Data;
using System.Collections.ObjectModel;

namespace Game2048.ViewModels
{
    internal class StatisticsViewModel : ViewModel
    {
        public NavigationCommand NavigateToMenuPageCommand { get => new(NavigateToPage, new Uri("Views/Pages/MainMenuPage.xaml", UriKind.RelativeOrAbsolute)); }

        public static ObservableCollection<Player> StatisticsCollection { get => Statistics.Players;  }
    }
}