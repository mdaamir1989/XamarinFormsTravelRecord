using System;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        RegisterVM ViewModel { get; set; }
        public event EventHandler CanExecuteChanged;

        public RegisterCommand(RegisterVM vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            var user = (User)parameter;

            // Check user null
            if (user == null)
                return false;

            // Check properties null
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.ConfirmPassword))
                return false;

            // Check for passwword and confirm password match.
            if (user.Password != user.ConfirmPassword)
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            ViewModel.Register();
        }
    }
}
