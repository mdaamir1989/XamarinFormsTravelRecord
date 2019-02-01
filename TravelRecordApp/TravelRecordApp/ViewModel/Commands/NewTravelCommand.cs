using System;
using System.Windows.Input;
using TravelRecordApp.Model;

namespace TravelRecordApp.ViewModel.Commands
{
    public class NewTravelCommand : ICommand
    {
        NewTravelVM ViewModel;
        public event EventHandler CanExecuteChanged;

        public NewTravelCommand(NewTravelVM vm)
        {
            ViewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            //return true;
            var post = (Post)parameter;

            if (post != null)
            {
                if (string.IsNullOrEmpty(post.Experience))
                    return false;

                if (post.Venue != null)
                    return true;

                return false;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            var post = (Post)parameter;
            ViewModel.AddNewTravel(post);
        }
    }
}
