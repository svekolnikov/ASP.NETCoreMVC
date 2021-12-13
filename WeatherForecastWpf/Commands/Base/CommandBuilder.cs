using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherForecastWpf.Commands.Base
{
    public class CommandBuilder 
    {
        private Func<object?, Task> _execute;
        private Func<object?, bool>? _canExecute;

        public CommandBuilder(){}
        public CommandBuilder(Func<object?, Task> execute) => 
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));

        public CommandBuilder When(Func<object?, bool> canExecute)
        {
            _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
            return this;
        }

        public CommandBuilder Invoke(Func<object?, Task> execute)
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
