using System;
using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
using FlaUiTests.Helper;
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
        private ICalculatorElements _calculator;
        private UIOperations _uiOperations;

        /// <summary>
        /// Timeout to find the main window of the calculator application.
        /// </summary>
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
            _calculator = new Windows11CalculatorElements(_mainWindow);
            _uiOperations = new UIOperations(_calculator, _conditionFactory);
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
                AutomationElement targetModeItem = _uiOperations.GetListItem($"{targetMode} Calculator");

                if (targetModeItem == null)
                {
                    throw new NullReferenceException($"{targetMode} Calculator list item not found.");
                }
                _uiOperations.SelectListItem(targetModeItem);
            }
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
                try
                {
                    List<Button> buttons = inputFormatter.ExtractButtons(testCase);
                    foreach (Button button in buttons)
                    {
                        _uiOperations.ClickButton(button);
                    }
                    var resultText = _calculator.ResultText;

                    testCase.ActualResult = double.Parse(resultText);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error while performing test case: {testCase.ToString()}", ex);
                }
            }
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