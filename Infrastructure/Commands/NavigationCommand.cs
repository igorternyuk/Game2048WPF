using Game2048.Infrastructure.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Game2048.Infrastructure.Commands
{
    class NavigationCommand : BaseCommand
    {
        private readonly Action<Page, Uri> _Execute;
        private readonly Uri _Uri;
        public NavigationCommand(Action<Page, Uri> execute, Uri uri)
        {
            _Execute = execute;
            _Uri = uri;
        }
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            _Execute.Invoke((Page)parameter, _Uri);
        }
    }
}
