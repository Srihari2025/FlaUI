namespace FlaUiTests.Models
{
    /// <summary>
    /// Represents a test case for the calculator application.
    /// </summary>
    public class CalculatorTestCase
    {
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
        public double ExpectedResult { get; set; }

        /// <summary>
        /// Represents the actual result of the calculation after performing the operation.
        /// </summary>
        public double ActualResult { get; set; }
    }
}
