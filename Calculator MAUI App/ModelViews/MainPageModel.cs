using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Calculator_MAUI_App.ModelViews
{
    class MainPageModel : INotifyPropertyChanged
    {
        private string screenVal;
        private readonly List<string> availableOperations = new() { "+", "-", "/", "*" };
        private readonly DataTable dataTable = new();
        private bool isLastSignAnOperation;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageModel()
        {
            ScreenDisplay = "0";
            BackspaceCommand = new RelayCommand(Backspace);
            AddNumberCommand = new RelayCommand(AddNumber);
            AddOperationCommand = new RelayCommand(AddOperation, CanAddOperation);
            ClearScreenCommand = new RelayCommand(ClearScreen);
            GetResultCommand = new RelayCommand(GetResult, CanGetResult);
        }

        private void Backspace(object obj)
        {
            ScreenDisplay = ScreenDisplay.Substring(0, ScreenDisplay.Length - 1);
            if (ScreenDisplay == "")
            {
                ScreenDisplay = "0";
                isLastSignAnOperation = false;
            }
        }

        private bool CanGetResult(object obj) => !isLastSignAnOperation;
        private bool CanAddOperation(object obj) => !isLastSignAnOperation;

        private void GetResult(object obj)
        {
            var result = Math.Round(Convert.ToDouble(dataTable.Compute(ScreenDisplay.Replace(",", "."), "")), 2);

            ScreenDisplay = result.ToString();
        }

        private void ClearScreen(object obj)
        {
            ScreenDisplay = "0";

            isLastSignAnOperation = false;
        }

        private void AddOperation(object obj)
        {
            var operation = obj as string;

            ScreenDisplay += operation;

            isLastSignAnOperation = true;
        }

        private void AddNumber(object obj)
        {
            var number = obj as string;

            if (ScreenDisplay == "0" && number != ",")
                ScreenDisplay = string.Empty;
            else if (number == "," && availableOperations.Contains(ScreenDisplay.Substring(ScreenDisplay.Length - 1)))
                number = "0,";

            ScreenDisplay += number;

            isLastSignAnOperation = false;
        }

        public ICommand BackspaceCommand { get; private set; }
        public ICommand AddNumberCommand { get; private set; }
        public ICommand AddOperationCommand { get; private set; }
        public ICommand ClearScreenCommand { get; private set; }
        public ICommand GetResultCommand { get; private set; }

        public string ScreenDisplay
        {
            get { return screenVal; }
            set
            {
                screenVal = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
