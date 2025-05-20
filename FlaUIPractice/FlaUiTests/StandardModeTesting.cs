using System;
using System.Collections.Generic;
using FlaUiTests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlaUiTests.Helper;
using System.IO;
using System.Linq;

namespace FlaUiTests
{
    [TestClass]
    public class StandardModeTesting
    {
        private CalculatorTester _calculatorTester;
        private readonly int numberOfColumns = 6; // Number of columns in the CSV file
        private readonly string _filePath = @"..\..\Resources\TestDataSource.csv";

        [TestInitialize]
        public void Setup()
        {
            _calculatorTester = new CalculatorTester();
            try
            {
                _calculatorTester.SwitchCalculatorMode("Standard");
            }
            catch (NullReferenceException ex)
            {
                Assert.Fail($"Failed to switch to Standard mode: {ex.Message}");
            }
            catch 
            {
                Assert.Fail("Failed to launch the calculator application.");
            }
        }

        [TestMethod]
        public void TestCalculatorInStandardMode()
        {
            // Arrange
            List<CalculatorTestCase> testCases = ReadTestCases();
            Assert.IsNotNull(testCases, "Test cases data is null");

            // Act
            _calculatorTester.PerformTestCases(testCases);

            // Assert
            WriteTestCases(testCases);
            Assert.IsTrue(testCases.TrueForAll(result => result.IsPassed), "Some test cases failed.");
        }

        [TestCleanup]
        public void TearDown()
        {
            // Close the calculator application
            _calculatorTester.CloseApplication();
        }

        private List<CalculatorTestCase> ReadTestCases()
        {
            List<CalculatorTestCase> testCases = new List<CalculatorTestCase>();
            try
            {
                // Read the CSV file
                List<string[]> csvData = FileHandler.ReadCsv(_filePath);
                if (csvData == null || !csvData.Any())
                {
                    Assert.Fail("Test data is empty or null");
                }
                testCases = FormatBinaryOperationTestData(csvData);
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
                // Skip the header row
                for (int i = 1; i < data.Count; i++)
                {
                    var row = data[i];
                    if (row.Length == numberOfColumns)
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

        private void WriteTestCases(List<CalculatorTestCase> testCases)
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