using System;
using System.Collections.Generic;
using System.Windows.Documents;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.AutomationElements.PatternElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Exceptions;
using FlaUI.UIA3;
using FlaUiTests.Models;

namespace FlaUiTests
{
    /// <summary>
    /// This class is responsible for managing the calculator application.
    /// </summary>
    public class CalculatorTester
    {
        private FlaUI.Core.Application _application;
        private UIA3Automation _automation;
        private AutomationElement _mainWindow;
        private ConditionFactory _conditionFactory;
        private ICalculator _calculator;
        public double TimeOutToFindMainWindow { get; set; } = 2; //setting default timeout to 2 seconds

        /// <summary>
        /// Constructor for CalculatorTester, launches the calculator application and initializes the automation elements.
        /// </summary>
        public CalculatorTester()
        {
            _application = FlaUI.Core.Application.LaunchStoreApp("Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            _automation = new UIA3Automation();
            _mainWindow = _application.GetMainWindow(_automation, TimeSpan.FromSeconds(TimeOutToFindMainWindow));
            _conditionFactory = new ConditionFactory(new UIA3PropertyLibrary());
            _calculator = new Windows11Calculator(_mainWindow);
        }

        /// <summary>
        /// Switches the calculator mode to the specified mode.
        /// </summary>
        /// <param name="targetMode">Mode of Calculator</param>
        public void SwitchCalculatorMode(string targetMode)
        {
            if (_calculator.CalculatorMode.Name != $"{targetMode} Calculator mode")
            {
                _calculator.ButtonMenu.Click();
                if (_calculator.MenuItems == null)
                {
                    throw new NullReferenceException("Menu container not found.");
                }
                AutomationElement targetModeItem = GetListItem($"{targetMode} Calculator");

                if (targetModeItem == null)
                {
                    throw new NullReferenceException($"{targetMode} Calculator list item not found.");
                }
                SelectListItem(targetModeItem);
            }
        }

        /// <summary>
        /// Selects the list item in the calculator menu.
        /// </summary>
        /// <param name="targetModeItem">Target Mode Item</param>
        /// <exception cref="InvalidOperationException">Throws exception if the item is not selectable.</exception>
        /// <exception cref="ElementNotEnabledException">Throws exception if the item is not enabled.</exception>
        public void SelectListItem(AutomationElement targetModeItem)
        {
            try
            {
                targetModeItem.AsListBoxItem().Select();
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Failed to select the list item: {ex.Message}");
            }
            catch (ElementNotEnabledException ex)
            {
                throw new ElementNotEnabledException($"The list item is not enabled: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets the list item from the calculator menu.
        /// </summary>
        /// <param name="listItem">List item name</param>
        /// <returns>Returns listItem Automation element</returns>
        public AutomationElement GetListItem(string listItem)
        {
            return _calculator.MenuItems.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.ListItem).And(_conditionFactory.ByName(listItem)));
        }

        /// <summary>
        /// Performs the test cases by clicking the buttons in the calculator.
        /// </summary>
        /// <param name="testCases">List of testcases</param>
        public void PerformTestCases(List<CalculatorTestCase> testCases)
        {
            InputFormatter inputFormatter = new InputFormatter(_calculator);
            foreach (CalculatorTestCase testCase in testCases)
            {
                List<Button> buttons = inputFormatter.ExtractButtons(testCase);
                foreach (Button button in buttons)
                {
                    ClickButton(button);
                }
                var resultText = _calculator.ResultText;

                //Get only the result number after Display is "", convert it into double and store it in the Actual Result column of file.
                testCase.ActualResult = double.Parse(resultText);
            }
        }

        public void ClickButton(Button button)
        {
            button.Click();
        }

        /// <summary>
        /// Closes the calculator application.
        /// </summary>
        public void CloseApplication()
        {
            // Close the calculator application
            _automation.Dispose();
            _application.Close();
        }
    }
}
