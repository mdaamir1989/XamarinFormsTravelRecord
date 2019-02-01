using System;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public MainVM ViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public LoginCommand(MainVM vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            var user = (User)parameter;

            if (user == null)
                return false;

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                return false;
            else
                return true;
        }

        public void Execute(object parameter)
        {
            var user = (User)parameter;

            if (user != null)
                ViewModel.Login();
            else
                ViewModel.Register();
        }
    }
}
