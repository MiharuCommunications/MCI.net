namespace Miharu.Wpf.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    [Obsolete("未実装")]
    public class DelegateCommand<T> : ICommand
    {
        private Action<T> execute;

        private Func<T, bool> canExecute;


        public DelegateCommand(Action<T> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = execute;
            this.canExecute = param => true;
        }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            if (parameter is T)
            {
                return this.canExecute((T)parameter);
            }
            else
            {
                return this.canExecute(default(T));
            }
        }


        public bool CanExecute(T parameter)
        {
            return this.canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }


        public void Execute(object parameter)
        {
            if (parameter is T)
            {
                this.execute((T)parameter);
            }
            else
            {
                this.execute(default(T));
            }
        }


        public void Execute(T parameter)
        {
            this.execute(parameter);
        }
    }





    [Obsolete("未実装")]
    public class DelegateCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;


        public DelegateCommand(Action execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = param => execute();
            this.canExecute = param => true;
        }

        public DelegateCommand(Action<object> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = execute;
            this.canExecute = param => true;
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            this.execute = param => execute();
            this.canExecute = param => canExecute();
        }


        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }


        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        public bool CanExecute()
        {
            return this.canExecute(null);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }


        public void Execute()
        {
            this.execute(null);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
