# FlaUI Testing on Calculator

**Overview :**
- This test library can be used to perform UI testing on the **Standard** mode of Windows Calculator.
- It supports binary operations using simple arthimetic operators such as Addition, Subtraction, Multiplication and Division.
- The UI Automations are performed on the Calculator Application using FlaUI. [Know more](https://docs.google.com/document/d/1J81neFboMnpdDmU-KBTnSdujf7LQ52M2/edit?usp=sharing&ouid=111179004960829358587&rtpof=true&sd=true)
- For Demo video - [Click Here](https://drive.google.com/file/d/1_bG3pqpk_VzQFnDZagP0kd8BgiYNDQmh/view?usp=sharing)

**Software Requirements:**
- Visual Studio 2022
- FlaUI.Core, FlaUI.UIA3
- Windows 10 or later (for calculator compatibility)
- Windows Calculator Application
- .NET Framework 4.7.2 or later 

<img height="50" src="https://github.com/user-attachments/assets/a3b58d87-a855-4cb4-98d5-21ff0b8ad2dc">
<img height="50" src="https://github.com/user-attachments/assets/4b87d884-6501-4f44-8c38-2aa2225f13aa">

[![.NET](https://skillicons.dev/icons?i=visualstudio,dotnet)](https://skillicons.dev)

## To Get Started

Step 1 : Clone the repository to your local machine. <br>
Step 2 : Open the solution in Visual Studio. <br>
Step 3 : Build the solution to restore the required packages. <br>
Step 4 : Run the test cases using the Test Explorer in Visual Studio. <br>
Step 5 : The tests will open the Calculator application, perform the operations, and validate the results. <br>
Step 6 : The test results will be displayed in the [TestDataSource](FlaUIPractice/FlaUiTests/Resources/TestDataSource.csv) file. <br>
Step 7 : You can modify the test cases to add more operations or change the input values as needed. <br>

## Demonstration

As shown below, the test cases are executed. The Calculator application gets opened, the calculations are performed, and the results are validated. The test cases are designed to cover various scenarios, including addition, subtraction, multiplication, and division operations. The actual results obtained from the Calculator application are compared with the expected results to record the Test Result.

<p align="center" width="100%">
	<img height="400" src="https://github.com/user-attachments/assets/f2ffabac-f5a5-4b94-b224-29685bca5983">
</p>

## Test Results

The test results are stored in a CSV file named **TestDataSource.csv**. This file contains the input values, expected results, actual results, and the status of each test case (Pass/Fail).

<p align="center" width="100%">
	Before running tests 
	&emsp; &emsp; &emsp; &emsp; &emsp; &emsp; &emsp; &emsp; &emsp; &emsp; &emsp;
	After running tests
</p>
<p align="center" width="100%">
	<img width="45%" height="400" src="https://github.com/user-attachments/assets/2ca0869d-1f4b-4701-8150-2cff19c8c5f8">
	<img width="45%" height="400" src="https://github.com/user-attachments/assets/b62d0b78-f71a-42e1-b4fc-474a8da006ff">
</p>

## Implementation Details

**Folder Structure:**

<p align="left" width="100%">
	<img width="25%" height="250" src="https://github.com/user-attachments/assets/3bffc33e-a5dd-4f02-b3f8-af6ee24d1548">
</p>

**File Descriptions:**

- [File Handler](FlaUIPractice/FlaUiTests/Helper/FileHandler.cs) is used to read and write test case data to a file. It provides methods for reading the test case data from a file and writing the test results to a file. The file used to store the test case data is a Microsoft Excel CSV file, which is easy to read and write. The test case data is stored in a structured format, making it easy to parse and process.
- [InputFormatter](FlaUIPractice/FlaUiTests/Helper/InputFormatter.cs) is a utility class that formats the input values for the Calculator application. It ensures that the input values are in the correct format for testing by converting the numerics and symbols to the collection of Buttons that are to be clicked.
- [UI Operations](FlaUIPractice/FlaUiTests/Helper/UIOperations.cs) contains method that perform the UI operations on the calculator such as Button Click.
- [CalculatorTestCase Model](FlaUIPractice/FlaUiTests/Models/CalculatorTestCase.cs) is a model class that represents a test case for the Calculator application. It contains properties for the input values, expected result, and actual result.
- [ICalculatorElements](FlaUIPractice/FlaUiTests/Models/ICalculatorElements.cs) is the interface for UI elements implementations. Having an interface allows for easy extensibility and maintainability of the code. As there are multiple implementations of the Calculator application in Windows across different versions, having an interface allows for easy switching between different implementations without changing the code that uses it.
- [Windows11CalculatorElements](FlaUIPractice/FlaUiTests/Models/Windows11CalculatorElements.cs) is a class that implements the ICalculator interface having definition for the UI elements of Calculator.
- [Test Data Source](FlaUIPractice/FlaUiTests/Resources/TestDataSource.csv) is a CSV file that contains the test case data for the Calculator application. It includes the input values, expected results, and operation types for each test case. The CSV file is used as a data source for the test cases, allowing for easy modification and addition of new test cases without changing the code.
- [CalculatorTester](FlaUIPractice/FlaUiTests/CalculatorTester.cs) handles FlaUI testing methods and implements the testing logic methods.
- [StandardModeTests](FlaUIPractice/FlaUiTests/StandardModeTesting.cs) contains the test cases for the Standard mode of Windows Calculator.

## References
[FlaUI GitHub Repo](https://github.com/FlaUI/FlaUI)

[Self Made Notes](https://docs.google.com/document/d/1J81neFboMnpdDmU-KBTnSdujf7LQ52M2/edit?usp=sharing&ouid=111179004960829358587&rtpof=true&sd=true)
