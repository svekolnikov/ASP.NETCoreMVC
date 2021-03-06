using System;
using System.Threading.Tasks;
using WeatherForecastWpf.Commands.Base;

namespace WeatherForecastWpf.Commands
{
    internal class LambdaCommand : Command
    {
        private readonly Func<object?, Task> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public LambdaCommand(Func<object?, Task> execute, Func<object?, bool>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object? parameter) =>
            base.CanExecute(parameter)
            && (_canExecute?.Invoke(parameter) ?? true);

        public override void Execute(object? parameter) => _execute(parameter);
    }
}
