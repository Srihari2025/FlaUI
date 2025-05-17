using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Infrastructure;

namespace FlaUiTests
{
    /// <summary>
    /// Implementation of the calculator interface for Windows 11 Calculator.
    /// </summary>
    public class Windows11Calculator : ICalculator
    {
        private AutomationElement _mainWindow;

        /// <summary>
        /// Constructor for Windows11Calculator accepting the main window element.
        /// </summary>
        /// <param name="mainWindow">Mainwindow element</param>
        public Windows11Calculator(AutomationElement mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public Button Button1 => _mainWindow.FindFirstDescendant(cf => cf.ByName("One")).AsButton();
        public Button Button2 => _mainWindow.FindFirstDescendant(cf => cf.ByName("Two")).AsButton();
        public Button Button3 => _mainWindow.FindFirstDescendant(cf => cf.ByName("Three")).AsButton();
        public Button Button4 => _mainWindow.FindFirstDescendant(cf => cf.ByName("Four")).AsButton();
        public Button Button5 => _mainWindow.FindFirstDescendant(cf => cf.ByName("Five")).AsButton();
        public Button Button6 => _mainWindow.FindFirstDescendant(cf => cf.ByName("Six")).AsButton();
        public Button Button7 => _mainWindow.FindFirstDescendant(cf => cf.ByName("Seven")).AsButton();
        public Button Button8 => _mainWindow.FindFirstDescendant(cf => cf.ByName("Eight")).AsButton();
        public Button Button9 => _mainWindow.FindFirstDescendant(cf => cf.ByName("Nine")).AsButton();
        public Button Button0 => _mainWindow.FindFirstDescendant(cf => cf.ByName("Zero")).AsButton();
        public Button ButtonPlus => _mainWindow.FindFirstDescendant(cf => cf.ByName("Plus")).AsButton();
        public Button ButtonMinus => _mainWindow.FindFirstDescendant(cf => cf.ByName("Minus")).AsButton();
        public Button ButtonMultiply => _mainWindow.FindFirstDescendant(cf => cf.ByName("Multiply by")).AsButton();
        public Button ButtonDivide => _mainWindow.FindFirstDescendant(cf => cf.ByName("Divide by")).AsButton();
        public Button ButtonClear => _mainWindow.FindFirstDescendant(cf => cf.ByName("Clear")).AsButton();
        public Button ButtonEquals => _mainWindow.FindFirstDescendant(cf => cf.ByName("Equals")).AsButton();
        public Button ButtonDecimal => _mainWindow.FindFirstDescendant(cf => cf.ByName("Decimal separator")).AsButton();
    }
}
