namespace EyeTrackingHelper
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Delegate method that handles the invocation of
    /// the command.
    /// </summary>
    /// <param name="parameter">Parameter object for the command.</param>
    public delegate void CommandOnExecute(object parameter);

    /// <summary>
    /// Delegate method that answers the question whether 
    /// command can execute or not.
    /// </summary>
    /// <param name="parameter">Parameter object for the command.</param>
    /// <returns>A value indicating whether the command can be executed or not.</returns>
    public delegate bool CommandOnCanExecute(object parameter);

    /// <summary>
    /// Implementation of the <see cref="ICommand" /> interface.
    /// </summary>
    public class Command : ICommand
    {
        private readonly CommandOnExecute _execute;
        private readonly CommandOnCanExecute _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command" /> class.
        /// </summary>
        /// <param name="onExecuteMethod">Method that handles the invocation of the command.</param>
        /// <param name="onCanExecuteMethod">Method that handles can execute requests.</param>
        public Command(CommandOnExecute onExecuteMethod, CommandOnCanExecute onCanExecuteMethod)
        {
            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod;
        }

        #region ICommand Members

        /// <summary>
        /// Raised when the state of can execute might have changed.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Passes the can execute question on to the delegate.
        /// </summary>
        /// <param name="parameter">Parameter object for the command.</param>
        /// <returns>A value indicating whether the command can be executed or not.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke(parameter);
        }

        /// <summary>
        /// Passes on the execute message to the delegate.
        /// </summary>
        /// <param name="parameter">Parameter object for the command.</param>
        public void Execute(object parameter)
        {
            _execute.Invoke(parameter);
        }

        #endregion
    }
}
