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

        public Button Button1 => GetButtonByName("One");
        public Button Button2 => GetButtonByName("Two");
        public Button Button3 => GetButtonByName("Three");
        public Button Button4 => GetButtonByName("Four");
        public Button Button5 => GetButtonByName("Five");
        public Button Button6 => GetButtonByName("Six");
        public Button Button7 => GetButtonByName("Seven");
        public Button Button8 => GetButtonByName("Eight");
        public Button Button9 => GetButtonByName("Nine");
        public Button Button0 => GetButtonByName("Zero");
        public Button ButtonPlus => GetButtonByName("Plus");
        public Button ButtonMinus => GetButtonByName("Minus");
        public Button ButtonMultiply => GetButtonByName("Multiply by");
        public Button ButtonDivide => GetButtonByName("Divide by");
        public Button ButtonClear => GetButtonByName("Clear");
        public Button ButtonEquals => GetButtonByName("Equals");
        public Button ButtonDecimal => GetButtonByName("Decimal separator");
        public Button ButtonMenu => GetButtonByAutomationId("TogglePaneButton");
        public string ResultText
        {
            get
            {
                var resultText = _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("CalculatorResults"));
                return resultText.Name.Remove(0, 11);
            }
        }
        public AutomationElement MenuItems => GetButtonByAutomationId("MenuItemsScrollViewer");
        public AutomationElement CalculatorMode => _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("Header")).AsTextBox();
        private Button GetButtonByName(string buttonName)
        {
            return _mainWindow.FindFirstDescendant(cf => cf.ByName(buttonName)).AsButton();
        }

        private Button GetButtonByAutomationId(string automationId)
        {
            return _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId(automationId)).AsButton();
        } 
    }
}