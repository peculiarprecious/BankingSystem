# Advanced Banking System

# Author
Precious Nwajei

# Description

A comprehensive C# console application designed to simulate a professional banking system. This project demonstrates core Object-Oriented Programming (OOP) principles, advanced C# features, and algorithmic recursion.

## Features

### 1. Account Management (OOP & Encapsulation)
*   Secure Storage: Uses private fields and  properties to ensure data integrity.
*   Transaction History: Every deposit, withdrawal, and transfer is logged with a real-time timestamp.
*   Input Validation: Robust error handling for non-numeric inputs and insufficient funds.

### 2. Transaction Engine (Overloading & Parameters)
*   Method Overloading: Multiple `Transfer` and `CalculateInterest`  to handle different business scenarios.
*   Parameter Passing: Demonstrates `Value`, `ref`, `out`, and `Optional/Named` parameters for efficient data handling.
*  Batch Processing: Ability to apply interest to all accounts simultaneously via the Statistics module.

### 3. Loan Calculator (Recursive Algorithms)
*   Recursive Payment Schedule: Generates a month-by-month breakdown of loan payments.
*   Recursive Compound Interest: Calculates long-term wealth growth using recursive math.
*   Recursive Array Summation: Navigates account arrays to calculate total bank assets.

## Technical Stack
*   Language: C# (.NET Core)
*   Concepts: Encapsulation, Method Overloading, Chaining, Recursion, List Collections.
*   Tools: Git/GitHub for version control.

## How to Use
1. Startup: The system auto-generates 3 sample accounts for setup.
2. Main Menu: Navigate through 14 options using numeric input.
3. Validation: Try entering invalid text or negative amounts to see the robust error handling in action.

## Project Structure
*   `Program.cs`: The Main entry point and Menu UI logic.
*   `BankAccount.cs`: Core logic for deposits, withdrawals, and interest math.
*   `LoanCalculator.cs`: Static class for all recursive operations.
*   `TransactionProcessor.cs`: Demonstrates Pass-by-Ref/Value/Out.
*   `AccountManager.cs`: Handles administrative tasks with optional parameters.


*Developed as part of the DotNet Cohort 5 Training.*
