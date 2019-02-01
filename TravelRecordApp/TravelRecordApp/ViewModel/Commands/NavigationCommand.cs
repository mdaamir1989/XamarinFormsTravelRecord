using System;
using System.Windows.Input;

namespace TravelRecordApp.ViewModel.Commands
{
    public class NavigationCommand : ICommand
    {
        public HomeVM HomeViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public NavigationCommand(HomeVM homeVM)
        {
            HomeViewModel = homeVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            HomeViewModel.Navigate();
        }
    }
}
