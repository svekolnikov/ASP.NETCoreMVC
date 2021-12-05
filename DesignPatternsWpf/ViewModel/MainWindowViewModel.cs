using System.ComponentModel;
using System.Windows.Input;
using DesignPatternsWpf.Commands;

namespace DesignPatternsWpf.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private string _title;
        private string _inputText;
        private int _counter;

        public MainWindowViewModel()
        {
            Title = "Lesson_3";

            ClickCommand = new RelayCommand(ClickExecute, _ => CanClick);
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                NotifyPropertyChanged(nameof(InputText));
            }
        }
        public int Counter
        {
            get => _counter;
            set
            {
                _counter = value;
                NotifyPropertyChanged(nameof(Counter));
            }
        }

        public ICommand ClickCommand { get; set; }
        public bool CanClick => InputText == "start";
        private void ClickExecute(object obj)
        {
            Counter++;
        }
    }
}
