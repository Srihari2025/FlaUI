using FlaUI.Core.AutomationElements;

namespace FlaUiTests
{
    /// <summary>
    /// Interface for a calculator application buttons.
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Represents Number 1 button in calculator.
        /// </summary>
        Button Button1 { get; }

        /// <summary>
        /// Represents Number 2 button in calculator.
        /// </summary>
        Button Button2 { get; }

        /// <summary>
        /// Represents Number 3 button in calculator.
        /// </summary>
        Button Button3 { get; }

        /// <summary>
        /// Represents Number 4 button in calculator.
        /// </summary>
        Button Button4 { get; }

        /// <summary>
        /// Represents Number 5 button in calculator.
        /// </summary>
        Button Button5 { get; }

        /// <summary>
        /// Represents Number 6 button in calculator.
        /// </summary>
        Button Button6 { get; }

        /// <summary>
        /// Represents Number 7 button in calculator.
        /// </summary>
        Button Button7 { get; }

        /// <summary>
        /// Represents Number 8 button in calculator.
        /// </summary>
        Button Button8 { get; }

        /// <summary>
        /// Represents Number 9 button in calculator.
        /// </summary>
        Button Button9 { get; }

        /// <summary>
        /// Represents Number 0 button in calculator.
        /// </summary>
        Button Button0 { get; }

        /// <summary>
        /// Represents the addition button in calculator.
        /// </summary>
        Button ButtonPlus { get; }

        /// <summary>
        /// Represents the subtraction button in calculator.
        /// </summary>
        Button ButtonMinus { get; }

        /// <summary>
        /// Represents the multiplication button in calculator.
        /// </summary>
        Button ButtonMultiply { get; }

        /// <summary>
        /// Represents the division button in calculator.
        /// </summary>
        Button ButtonDivide { get; }

        /// <summary>
        /// Represents the equals button in calculator.
        /// </summary>
        Button ButtonEquals { get; }

        /// <summary>
        /// Represents the clear button in calculator.
        /// </summary>
        Button ButtonClear { get; }

        /// <summary>
        /// Represents the decimal button in calculator.
        /// </summary>
        Button ButtonDecimal { get; }
    }
}
