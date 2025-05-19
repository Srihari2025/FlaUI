using System;
using System.Collections.Generic;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;
using FlaUiTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlaUiTests.Helper;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Infrastructure;
using System.IO;

namespace FlaUiTests
{
    [TestClass]
    public class StandardModeTesting
    {
        private FlaUI.Core.Application _application;
        private UIA3Automation _automation;
        private AutomationElement _mainWindow;
        private ConditionFactory _conditionFactory;
        private ICalculator _calculator;
        private readonly string _filePath = @"..\..\Resources\TestDataSource.csv";

        [TestInitialize]
        public void Setup()
        {
            _application = FlaUI.Core.Application.LaunchStoreApp("Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            _automation = new UIA3Automation();
            _mainWindow = _application.GetMainWindow(_automation);
            _calculator = new Windows11Calculator(_mainWindow);
            _conditionFactory = new ConditionFactory(new UIA3PropertyLibrary());
        }

        [TestMethod]
        public void TestCalculatorInStandardMode()
        {
            // Arrange
            SwitchCalculatorMode("Standard");
            List<CalculatorTestCase> testCases = FetchTestCasesData();
            Assert.IsNotNull(testCases, "Test cases data is null");

            // Act
            PerformTestCases(testCases);

            // Assert
            WriteTestResultsToFile(testCases);
            Assert.IsTrue(testCases.TrueForAll(result => result.IsPassed), "Some test cases failed.");
        }



        [TestCleanup]
        public void TearDown()
        {
            // Close the calculator application
            _automation.Dispose();
            _application.Close();
        }

        private List<CalculatorTestCase> FetchTestCasesData()
        {
            List<CalculatorTestCase> testCases = new List<CalculatorTestCase>();
            try
            {
                List<string[]> data = FileHandler.ReadCsv(_filePath);
                if (data == null || data.Count == 0)
                {
                    Assert.Fail("Test data is empty or null");
                }
                testCases = FormatBinaryOperationTestData(data);
            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail($"File not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"An error occurred while reading the file: {ex.Message}");
            }
            return testCases;
        }

        private List<CalculatorTestCase> FormatBinaryOperationTestData(List<string[]> data)
        {
            List<CalculatorTestCase> testCases = new List<CalculatorTestCase>();
            try
            {
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
            }
            catch (ArgumentNullException ex)
            {
                Assert.Fail($"Null argument: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Assert.Fail($"Invalid data format: {ex.Message}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"An error occurred while formatting test data: {ex.Message}");
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
                var resultText = _calculator.ResultText;

                //Get only the result number after Display is "", convert it into double and store it in the Actual Result column of file.
                testCase.ActualResult = double.Parse(resultText);
            }
        }

        public void SwitchCalculatorMode(string targetMode)
        {
            //Ensure the Calculator is in Standard mode
            if (_calculator.CalculatorMode.Name != $"{targetMode} Calculator mode")
            {
                _calculator.ButtonMenu.Click();
                if (_calculator.MenuItems == null)
                {
                    Console.WriteLine("Menu container not found.");
                    return;
                }
                AutomationElement targetModeItem = _calculator.MenuItems.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.ListItem).And(_conditionFactory.ByName($"{targetMode} Calculator")));

                if (targetModeItem != null)
                {
                    targetModeItem.AsListBoxItem().Select();
                    Console.WriteLine($"{targetMode} Calculator clicked.");
                }
                else
                {
                    Console.WriteLine($"{targetMode} Calculator list item not found.");
                }
            }
        }

        private void WriteTestResultsToFile(List<CalculatorTestCase> testCases)
        {
            //Call the write csv method to write the test results to the file
            try
            {
                FileHandler.WriteCsv(_filePath, testCases.ConvertAll(tc => tc.ToString().Split(',')));
            }
            catch (FileNotFoundException ex)
            {
                Assert.Fail($"File not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"An error occurred while writing to the file: {ex.Message}");
            }
        }
    }
}