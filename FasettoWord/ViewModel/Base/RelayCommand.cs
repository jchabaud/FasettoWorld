using System;
using System.Windows.Input;

namespace FasettoWord
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        private Action mAction;
        public bool CanExecute(object parameter)
        {
            return true;
        }
     
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        public void Execute(object parameter)
        {
            mAction();
        }    
    }
}
