using Game2048.Infrastructure.Commands;
using Game2048.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048.ViewModels
{
    internal class MenuViewModel : ViewModel
    {
        public NavigationCommand NavigateToGamePage{ get => new(NavigateToPage, new Uri("Views/Pages/GamePage.xaml", UriKind.RelativeOrAbsolute)); }
        public static NavigationCommand NavigateToStatisticsPage { get => new(NavigateToPage, new Uri("Views/Pages/Statistics.xaml", UriKind.RelativeOrAbsolute));}

        public static LambdaCommand QuitCommand { get => new LambdaCommand((object p) => { Quit(); }, (object p) => true); }


    }
}
