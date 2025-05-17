using System;
using System.Collections.Generic;
using FlaUI.Core;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
using FlaUiTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlaUiTests.Helper;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Infrastructure;

namespace FlaUiTests
{
    [TestClass]
    public class StandardModeTesting
    {
        private AutomationElement _mainWindow;
        private ConditionFactory _conditionFactory;
        private ICalculator _calculator;
        private readonly string _filePath = @"..\..\Resources\TestDataSource.csv";

        [TestMethod]
        public void TestCalculatorInStandardMode()
        {
            // Arrange
            SetUpCalculatorInStandardMode();
            List<string[]> data = FileHandler.ReadCsv(_filePath);
            List<CalculatorTestCase> testCases = FetchTestCasesData(data);
            Assert.IsNotNull(testCases, "Test cases data is null");

            //Act
            PerformTestCases(testCases);

            //Assert
            List<bool> testResults = ValidateTestCases(testCases);
            UpdateTestCasesResults(data, testCases, testResults);
            FileHandler.WriteCsv(_filePath, data);
        }

        private void SetUpCalculatorInStandardMode()
        {
            //Ensure the Calculator is in Standard mode
            Application application = FlaUI.Core.Application.LaunchStoreApp("Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            var automation = new UIA3Automation();
            var mainWindow = application.GetMainWindow(automation);

            _mainWindow = mainWindow;
            _calculator = new Windows11Calculator(mainWindow);

            ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());
            _conditionFactory = cf;

            var calculatorMode = mainWindow.FindFirstDescendant(cf.ByAutomationId("Header")).AsTextBox();
            if (calculatorMode.Name != "Standard Calculator mode")
            {
                var menuButton = mainWindow.FindFirstDescendant(cf.ByAutomationId("TogglePaneButton")).AsButton();
                menuButton.Click();

                var menuItems = mainWindow.FindFirstDescendant(cf1 => cf1.ByAutomationId("MenuItemsScrollViewer"));

                if (menuItems == null)
                {
                    Console.WriteLine("Menu container not found.");
                    return;
                }

                var standardItem = menuItems.FindFirstDescendant(cf1 => cf1.ByControlType(FlaUI.Core.Definitions.ControlType.ListItem).And(cf.ByName("Standard Calculator")));

                if (standardItem != null)
                {
                    standardItem.AsListBoxItem().Select();
                    Console.WriteLine("Standard Calculator clicked.");
                }
                else
                {
                    Console.WriteLine("Standard Calculator list item not found.");
                }
            }
        }

        private List<CalculatorTestCase> FetchTestCasesData(List<string[]> data)
        {
            List<CalculatorTestCase> testCases = new List<CalculatorTestCase>();
            for (int i = 1; i < data.Count; i++)
            {
                var row = data[i];
                if (row.Length == 6)
                {
                    var testCase = new CalculatorTestCase
                    {
                        Operand1 = double.Parse(row[0]),
                        Operand2 = double.Parse(row[1]),
                        Operator = row[2],
                        ExpectedResult = double.Parse(row[3])
                    };
                    testCases.Add(testCase);
                }
            }
            return testCases;
        }

        private void PerformTestCases(List<CalculatorTestCase> testCases)
        {
            InputFormatter inputFormatter = new InputFormatter(_calculator);
            foreach (CalculatorTestCase testCase in testCases)
            {
                List<Button> buttons = inputFormatter.ExtractButtons(testCase);
                foreach (Button button in buttons)
                {
                    button.Click();
                }
                var resultText = _mainWindow.FindFirstDescendant(_conditionFactory.ByAutomationId("CalculatorResults"));

                //Get only the result number after Display is "", convert it into double and store it in the Actual Result column of file.
                var resultNumber = double.Parse(resultText.Name.Remove(0, 10));
                testCase.ActualResult = Math.Round(resultNumber, 9);
            }
        }

        private List<bool> ValidateTestCases(List<CalculatorTestCase> testCases)
        {
            //Check the actual result with the expected result upto 9 decimal places with rounding
            List<bool> results = new List<bool>();
            foreach (CalculatorTestCase testCase in testCases)
            {
                //Compare the actual result with the expected result
                bool isPassed = Math.Round(testCase.ActualResult, 9) == Math.Round(testCase.ExpectedResult, 9);
                results.Add(isPassed);
            }
            return results;
        }


        private void UpdateTestCasesResults(List<string[]> data, List<CalculatorTestCase> testCases, List<bool> results)
        {
            for (int i = 1; i < data.Count; i++)
            {
                data[i][4] = testCases[i - 1].ActualResult.ToString();
                data[i][5] = results[i - 1] ? "Pass" : "Fail";
            }
        }
    }
}