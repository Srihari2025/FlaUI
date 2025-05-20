using System;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Conditions;
using FlaUI.Core.Exceptions;

namespace FlaUiTests.Helper
{
    /// <summary>
    /// This class contains the UI operations for the calculator application.
    /// </summary>
    public class UIOperations
    {
        private ICalculatorElements _calculatorElements;
        private ConditionFactory _conditionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="UIOperations"/> class.
        /// </summary>
        /// <param name="calculatorElements">Calculator elements</param>
        /// <param name="conditionFactory">Condition factory</param>
        public UIOperations(ICalculatorElements calculatorElements, ConditionFactory conditionFactory)
        {
            _calculatorElements = calculatorElements;
            _conditionFactory = conditionFactory;
        }

        /// <summary>
        /// Gets the list item from the calculator menu.
        /// </summary>
        /// <param name="listItem">List item name</param>
        /// <returns>Returns listItem Automation element</returns>
        public AutomationElement GetListItem(string listItem)
        {
            return _calculatorElements.MenuItems?.FindFirstDescendant(cf => cf.ByControlType(FlaUI.Core.Definitions.ControlType.ListItem)?.And(_conditionFactory.ByName(listItem)));
        }

        /// <summary>
        /// Clicks the button in the calculator.
        /// </summary>
        /// <param name="button">Button in calculator</param>
        public void ClickButton(Button button)
        {
            button.Click();
        }

        /// <summary>
        /// Selects the list item in the calculator menu.
        /// </summary>
        /// <param name="targetModeItem">Target Mode Item</param>
        /// <exception cref="InvalidOperationException">Throws exception if the item is not selectable.</exception>
        /// <exception cref="ElementNotEnabledException">Throws exception if the item is not enabled.</exception>
        public void SelectListItem(AutomationElement targetModeItem)
        {
            targetModeItem.AsListBoxItem().Select();
        }
    }
}