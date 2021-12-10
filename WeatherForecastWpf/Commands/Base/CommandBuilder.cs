using System;
using System.Windows.Input;

namespace WeatherForecastWpf.Commands.Base
{
    public class CommandBuilder 
    {
        private Action<object?> _execute;
        private Func<object?, bool>? _canExecute;

        public CommandBuilder(){}
        public CommandBuilder(Action<object?> execute) => 
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));

        public CommandBuilder When(Func<object?, bool> canExecute)
        {
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
            return this;
        }

        public CommandBuilder Invoke(Action<object?> execute)
        {
            _execute += execute;
            return this;
        }

        public ICommand Build()
        {
            return new LambdaCommand(_execute, _canExecute);
        }
        
        public static implicit operator Command(CommandBuilder builder) => (Command)builder.Build();
    }
}
