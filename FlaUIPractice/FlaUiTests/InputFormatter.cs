using FlaUI.Core.AutomationElements;
using FlaUiTests.Models;
using System;
using System.Collections.Generic;

namespace FlaUiTests
{
    /// <summary>
    /// This class is responsible for formatting the input for the calculator.
    /// </summary>
    public class InputFormatter
    {
        private readonly ICalculator _calculator;
        private readonly Dictionary<string, Button> _calculatorButtons;

        /// <summary>
        /// Constructor for InputFormatter accepting an ICalculator instance for extracting button data.
        /// </summary>
        /// <param name="calculator">Calculator instance</param>
        public InputFormatter(ICalculator calculator)
        {
            _calculator = calculator;
            _calculatorButtons = new Dictionary<string, Button>
            {
                { "ADD", _calculator.ButtonPlus },
                { "SUB", _calculator.ButtonMinus },
                { "MUL", _calculator.ButtonMultiply },
                { "DIV", _calculator.ButtonDivide },
                { "=", _calculator.ButtonEquals },
                { "1", _calculator.Button1},
                { "2", _calculator.Button2},
                { "3", _calculator.Button3},
                { "4", _calculator.Button4},
                { "5", _calculator.Button5},
                { "6", _calculator.Button6},
                { "7", _calculator.Button7},
                { "8", _calculator.Button8},
                { "9", _calculator.Button9},
                { "0", _calculator.Button0},
                {"+", _calculator.ButtonPlus },
                { "-", _calculator.ButtonMinus },
                {".", _calculator.ButtonDecimal },
                {"CLR", _calculator.ButtonClear }
            };
        }

        /// <summary>
        /// Returns the list of buttons to be clicked in the calculator for the given test case.
        /// </summary>
        /// <param name="testCase">Test case</param>
        /// <returns>List of buttons</returns>
        public List<Button> ExtractButtons(CalculatorTestCase testCase)
        {
            List<Button> buttons = new List<Button>();

            //Process the operand1
            buttons.AddRange(GetNumberButton(testCase.Operand1));

            //Process the operator
            buttons.Add(GetOperatorButton(testCase.Operator));

            //Process the operand2
            buttons.AddRange(GetNumberButton(testCase.Operand2));

            //Process the equals
            buttons.Add(_calculatorButtons["="]);

            return buttons;
        }

        private Button GetOperatorButton(string @operator)
        {
            if (_calculatorButtons.ContainsKey(@operator))
            {
                return _calculatorButtons[@operator];
            }
            else
            {
                throw new ArgumentException($"Operator {@operator} is not supported.");
            }
        }

        private List<Button> GetNumberButton(double operand)
        {
            List<Button> numberButtonsList = new List<Button>();
            string operandString = operand.ToString();
            for (int i = 0; i < operandString.Length; i++)
            {
                char c = operandString[i];
                if (c == '.')
                {
                    numberButtonsList.Add(_calculator.ButtonDecimal);
                }
                else if (char.IsDigit(c))
                {
                    int digit = c - '0';
                    numberButtonsList.Add(_calculatorButtons[Char.ToString(c)]);
                }
            }
            return numberButtonsList;
        }
    }
}
