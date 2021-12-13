using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherForecastWpf.Commands.Base
{
    public abstract class Command : ICommand
    {
        public static CommandBuilder Invoke(Func<object?, Task> execute) => new(execute);

        public static CommandBuilder New() => new();

        public static ICommand New(Func<object?, Task> execute, Func<object?, bool>? canExecute = null) =>
            new LambdaCommand(execute, canExecute);

        public static ICommand New(Func<Task> Execute, Func<bool>? CanExecute = null)
        {
            Func<object?, Task> execute = _ => Execute();
            Func<object?, bool>? canExecute = CanExecute is null ? null : _ => CanExecute();
            return new LambdaCommand(execute, canExecute);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public virtual bool CanExecute(object? parameter) => true;
        public abstract void Execute(object? parameter);
    }
}
