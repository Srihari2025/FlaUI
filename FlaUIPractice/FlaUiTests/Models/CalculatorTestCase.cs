using System;

namespace FlaUiTests.Models
{
    /// <summary>
    /// Represents a test case for the calculator application.
    /// </summary>
    public class CalculatorTestCase
    {
        private double _expectedResult;
        private double _actualResult;
        private const int _maxDecimalPlacesForPrecision = 9;

        /// <summary>
        /// Represents the first operand in the calculation.    
        /// </summary>
        public double Operand1 { get; set; }

        /// <summary>
        /// Represents the second operand in the calculation.
        /// </summary>
        public double Operand2 { get; set; }

        /// <summary>
        /// Represents the operator used in the calculation (e.g., "+", "-", "*", "/").
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Represents the expected result of the calculation.
        /// </summary>
        public double ExpectedResult
        {
            get => _expectedResult;
            set => _expectedResult = Math.Round(value, _maxDecimalPlacesForPrecision);
        }

        /// <summary>
        /// Represents the actual result of the calculation after performing the operation.
        /// </summary>
        public double ActualResult
        {
            get => _actualResult;
            set => _actualResult = Math.Round(value, _maxDecimalPlacesForPrecision);
        }

        /// <summary>
        /// Indicates whether the test case passed or failed based on the comparison of expected and actual results.
        /// </summary>
        public bool IsPassed
        {
            get
            {
                return Math.Abs(ExpectedResult - ActualResult) < Math.Pow(10, -_maxDecimalPlacesForPrecision);
            }
        }

        /// <summary>
        /// To string method to display the test case details in csv format.
        /// </summary>
        public override string ToString()
        {
            return $"{Operand1},{Operand2},{Operator},{ExpectedResult},{ActualResult},{(IsPassed == true ? "Pass" : "Fail")}";
        }
    }
}