using System;
using System.Windows.Input;

namespace TimerTab.Command
{
    class ActionCommand:ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="action">The action to invoke on command.</param>
        public ActionCommand(Action<object> action) : this(action, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="action">The action to invoke on command.</param>
        /// <param name="predicate">The predicate that determines if the action can be invoked.</param>
        public ActionCommand(Action<object> action, Predicate<object> predicate)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), @"You must specify an Action<T>.");
            }

            _action = action;
            _predicate = predicate;
        }

        /// <summary>
        /// Occurs when the <see cref="System.Windows.Input.CommandManager"/> detects conditions that might change the ability of a command to execute.
        /// </summary>
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

        /// <summary>
        /// Determines whether the command can execute.
        /// </summary>
        /// <param name="parameter">A custom parameter object.</param>
        /// <returns>
        ///     Returns true if the command can execute, otherwise returns false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            if (_predicate == null)
            {
                return true;
            }
            return _predicate(parameter);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            Execute(null);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">A custom parameter object.</param>
        public void Execute(object parameter) => _action(parameter);
    }
}
