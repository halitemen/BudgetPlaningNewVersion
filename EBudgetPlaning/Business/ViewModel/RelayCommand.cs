﻿using System;
using System.Windows.Input;

public class RelayCommand<T> : ICommand

{
    #region Fields

    readonly Action<T> _execute = null;
    readonly Predicate<T> _canExecute = null;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of <see cref="DelegateCommand{T}"/>.
    /// </summary>
    /// <param name="execute">Delegate to execute when Execute is called on the command.  This can be null to just hook up a CanExecute delegate.</param>
    /// <remarks><seealso cref="CanExecute"/> will always return true.</remarks>
    public RelayCommand(Action<T> execute)
        : this(execute, null)
    {
    }

    /// <summary>
    /// Creates a new command.
    /// </summary>
    /// <param name="execute">The execution logic.</param>
    /// <param name="canExecute">The execution status logic.</param>
    public RelayCommand(Action<T> execute, Predicate<T> canExecute)
    {
        if (execute == null)
            throw new ArgumentNullException("execute");

        _execute = execute;
        _canExecute = canExecute;
    }

    #endregion

    #region ICommand Members

    ///<summary>
    ///Defines the method that determines whether the command can execute in its current state.
    ///</summary>
    ///<param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    ///<returns>
    ///true if this command can be executed; otherwise, false.
    ///</returns>
    public bool CanExecute(object parameter)
    {
        return _canExecute == null ? true : _canExecute((T)parameter);
    }

    ///<summary>
    ///Occurs when changes occur that affect whether or not the command should execute.
    ///</summary>
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    ///<summary>
    ///Defines the method to be called when the command is invoked.
    ///</summary>
    ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
    public void Execute(object parameter)
    {
        _execute((T)parameter);
    }

    #endregion
}

public class RelayCommand : ICommand
{
    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
    private Action methodToExecute;
    private Func<bool> canExecuteEvaluator;
    public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
    {
        this.methodToExecute = methodToExecute;
        this.canExecuteEvaluator = canExecuteEvaluator;
    }
    public RelayCommand(Action methodToExecute)
        : this(methodToExecute, null)
    {
    }
    public bool CanExecute(object parameter)
    {
        if (this.canExecuteEvaluator == null)
        {
            return true;
        }
        else
        {
            bool result = this.canExecuteEvaluator.Invoke();
            return result;
        }
    }
    public void Execute(object parameter)
    {
        this.methodToExecute.Invoke();
    }
}
