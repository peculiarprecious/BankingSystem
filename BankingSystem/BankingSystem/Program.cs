// See https://aka.ms/new-console-template for more information
using System;

namespace BankingSystem
{
    class Program
    {
        private static List<BankAccount> _accounts = new List<BankAccount>();
        static void Main(string[] args)
        {

            DataSeedSamples();

            bool isActiveMenu = true;

            // Welcome Screen
            Console.WriteLine("**************************************");
            Console.WriteLine("Welcome to Advanced Banking System");
            Console.WriteLine($"System Date: {DateTime.Now:dd/MM/yyyy}");
            Console.WriteLine("**************************************");

            // Main Menu Loop
            while (isActiveMenu)
            {

                DisplayMenu();


                if (!int.TryParse(Console.ReadLine(), out int choice))
                {

                }

                switch (choice)
                {
                    case 1:
                        //Create New Account

                        CreateNewAccount();
                        break;
                    case 2:
                        //Deposit
                        Console.WriteLine("Enter amount to deposit");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                        {
                            Console.WriteLine("Invalid amount");
                            break;
                        }
                        BankAccount? accountToDeposit = FindAccount();
                        if (accountToDeposit != null)
                        {
                            accountToDeposit.Deposit(amount);

                            Console.WriteLine($"\nSuccess! Amount of {amount:C} has been deposited into Account Number {accountToDeposit.AccountNumber}.");
                            Console.WriteLine($"New Balance: {accountToDeposit.Balance:C} | Date & Time: {DateTime.Now:dd MMM yyyy HH:mm:ss}");
                        }
                        break;
                    case 3:
                        //Withdraw
                        Console.WriteLine("Enter amount to withdral");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal amountToWithdraw))
                        {
                            Console.WriteLine("Invalid amount");
                            break;
                        }
                        BankAccount? accountToWithdraw = FindAccount();
                        if (accountToWithdraw != null)
                        {
                            accountToWithdraw.Withdrawal(amountToWithdraw);

                            Console.WriteLine($"\nSuccess! Amount of {amountToWithdraw:C} has been withdrawn from Account Number {accountToWithdraw.AccountNumber}.");
                            Console.WriteLine($"New Balance: {accountToWithdraw.Balance:C} | Date & Time: {DateTime.Now:dd MMM yyyy HH:mm:ss}");
                        }
                        break;
                    case 4:
                        //display Individual account info
                        BankAccount? acc = FindAccount();
                        if (acc != null)
                        {
                            acc.DisplayAccountInfo();
                        }
                        break;
                    case 5:
                        //Display all Account Info
                        DisplayAccounts();
                        break;
                    case 6:
                        //Transfer Money (Overload
                        HandleTransfer();
                        break;
                    case 7:
                        //Calculate Interest
                        CalculateInterest();
                        break;
                    case 8:
                        //Display Payment Schedule (Recursive
                        HandlePaymentSchedule();
                        break;
                    case 9:
                        //Calculate Compound Interest (Recursive)
                        HandleRecursiveCompoundInterest();

                        break;
                    case 10:
                        //Sum Deposit Array (Recursive)
                        HandleSumDeposits();

                        break;

                    case 11:
                        //Test Pass by Value/Reference
                        HandleParameterTesting();


                        break;

                    case 12:
                        //Display Bank Statistics
                        DisplayBankStatistics();
                        break;

                    case 13:
                        HandleOptionalParameters();
                        break;
                    case 14:
                        isActiveMenu = false;
                        Console.WriteLine("Exiting system. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option, try again.");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n=== Advanced Banking System ===");
            Console.WriteLine("1. Create New Account");
            Console.WriteLine("2. Deposit Money");
            Console.WriteLine("3. Withdraw Money");
            Console.WriteLine("4. View Individual Account Info)");
            Console.WriteLine("5.View Account Info");
            Console.WriteLine("7. Calculate Interest");
            Console.WriteLine("8. Display Payment Schedule (Recursive)");
            Console.WriteLine("9. Calculate Compound Interest (Recursive)");
            Console.WriteLine("10. Sum Deposit Array (Recursive)");
            Console.WriteLine("11. Test Pass by Value/Reference");
            Console.WriteLine("12. Display Bank Statistics");
            Console.WriteLine("13. OptionalPramDemo");
            Console.WriteLine("14. Exit");
            Console.Write("\nSelect an option: ");
        }

        static void DataSeedSamples()
        {
            _accounts.Add(new BankAccount("Alice Johnson", "Savings", 5000m));
            _accounts.Add(new BankAccount("Bob Smith", "Savings", 3000m));
            _accounts.Add(new BankAccount("Charlie Davis", "Savings", 1500m));

            Console.WriteLine("Sample accounts created successfully.");
        }

        static void CreateNewAccount()
        {
            Console.Write("Enter Account Name:");

            string? accountName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(accountName))
            {
                Console.WriteLine("Error: Name cannot be empty.");
                return;
            }

            Console.WriteLine("Select Account Type:");
            Console.WriteLine("Savings");
            Console.WriteLine("Current");
            Console.WriteLine("Fixed");
            string? selection = Console.ReadLine()?.ToUpper();

            if (string.IsNullOrWhiteSpace(selection))
            {
                Console.WriteLine("Error: Please select Account type.");
                return;
            }

            Console.Write("Enter initial amount to deposit(Min:1000) : ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine($"\n Invalid amount.");
                return;
            }
            else if (amount < 1000)
            {
                Console.WriteLine("The minimun intial amount to deposite is 1000");
                return;
            }
            //Create the object using the validated data
            BankAccount newAccount = new BankAccount(accountName, selection, amount);

            // Add it to your master list
            _accounts.Add(newAccount);

            //Confirm to the user

            long accountNumber = newAccount.AccountNumber;


            Console.WriteLine($"\nSuccess! Account created for {accountName} with {amount:C}.");
            Console.WriteLine($"Your unique Account Number is: {accountNumber}");

        }

        static void DisplayAccounts()
        {
            if (_accounts.Count == 0)
            {
                Console.WriteLine("No record yet;");
                return;
            }

            Console.WriteLine($"{"Account Number",-15} | {"Account Holder",-20} | {"Account Type",-12} | {"Balance (£)",15} | {"Created Date",-15}");
            Console.WriteLine(new string('-', 90));
            foreach (var r in _accounts)
            {

                Console.WriteLine($"{r.AccountNumber,-15} | {r.AccountHolder,-20} | {r.AccountType,-12} | {r.Balance,15:C} | {DateTime.Now,-15:dd MMM yyyy}");
            }

        }

        static BankAccount? FindAccount()
        {
            Console.Write("Enter Account Number: ");

            //Validate that the input is actually a number
            if (!long.TryParse(Console.ReadLine()?.Trim(), out long targetNumber))
            {
                Console.WriteLine("Invalid input. Please enter a numerical account number.");
                return null;
            }

            // Search the list for the matching account
            BankAccount? foundAccount = _accounts.Find(a => a.AccountNumber == targetNumber);

            // Return the account if found, otherwise tell the user and return null
            if (foundAccount == null)
            {
                Console.WriteLine($"Account {targetNumber} not found.");
                return null;
            }

            return foundAccount;
        }
        static void HandleTransfer()
        {
            Console.WriteLine("\n--- Step 1: Select SENDER Account ---");
            BankAccount? sender = FindAccount();
            if (sender == null) return;

            Console.WriteLine("\n--- Step 2: Select RECEIVER Account ---");
            BankAccount? receiver = FindAccount();
            if (receiver == null) return;

            // Prevent transferring to the same account
            if (sender.AccountNumber == receiver.AccountNumber)
            {
                Console.WriteLine("Error: Source and destination accounts cannot be the same.");
                return;
            }

            Console.Write("Enter Transfer Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount. Transfer cancelled.");
                return;
            }

            int transferType;

            // This loop will repeat as long as the input is NOT a number OR not between 1 and 3
            while (true)
            {
                Console.WriteLine("\nChoose Transfer Type (1-3):");
                Console.WriteLine("1. Basic (Amount only)");
                Console.WriteLine("2. With Description");
                Console.WriteLine("3. Full (Description & Notification)");
                Console.Write("Selection: ");

                string? input = Console.ReadLine();

                // TryParse checks if it's an integer
                // We then check if that integer is a valid menu option (1, 2, or 3)
                if (int.TryParse(input, out transferType) && transferType >= 1 && transferType <= 3)
                {
                    break; // Input is valid, exit the loop
                }

                Console.WriteLine("Invalid entry! Please enter a number (1, 2, or 3).");
            }

            bool success = false;
            switch (transferType)
            {
                case 1:
                    success = sender.Transfer(receiver, amount);
                    break;
                case 2:
                    Console.Write("Enter Description: ");
                    string desc = Console.ReadLine() ?? "General Transfer";
                    success = sender.Transfer(receiver, amount, desc);
                    break;
                case 3:
                    Console.Write("Enter Description: ");
                    string fullDesc = Console.ReadLine() ?? "General Transfer";
                    Console.Write("Send Notifications? (y/n): ");
                    bool notify = Console.ReadLine()?.ToLower() == "y";
                    success = sender.Transfer(receiver, amount, fullDesc, notify);
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }

            if (success) Console.WriteLine("\nTransfer operation completed.");
        }

        static void CalculateInterest()
        {
            // 1. Find the account using your reusable helper
            BankAccount? account = FindAccount();
            if (account == null) return;

            // 2. Validate month input
            int months;
            while (true)
            {
                Console.Write("Enter number of months (1-12): ");
                if (int.TryParse(Console.ReadLine(), out months) && months > 0 && months <= 12) break;
                Console.WriteLine("Invalid entry! Please enter a number between 1 and 12.");
            }

            // 3. Choose the Overload
            Console.WriteLine("\nChoose Interest Type:");
            Console.WriteLine("1. Simple Interest (Default 2%)");
            Console.WriteLine("2. Simple Interest (Custom Rate)");
            Console.WriteLine("3. Compound Interest (Custom Rate)");
            Console.Write("Enter your choice:");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Invalid selection. Please choose 1, 2, or 3.");
            }

            decimal interestEarned = 0;

            switch (choice)
            {
                case 1:
                    interestEarned = account.CalculateInterest(months);
                    break;

                case 2:
                    Console.Write("Enter monthly rate (e.g., 0.05 for 5% or 5 for 5%): ");
                    decimal rate = 0;
                    if (decimal.TryParse(Console.ReadLine(), out decimal inputRate))
                    {
                        rate = (inputRate >= 1) ? inputRate / 100 : inputRate;
                    }
                    interestEarned = account.CalculateInterest(months, rate);
                    break;

                case 3:
                    Console.Write("Enter monthly rate (e.g., 0.05 or 5 for 5%): ");
                    decimal cRate = 0;
                    if (decimal.TryParse(Console.ReadLine(), out decimal inputCRate))
                    {
                        // Add this line to fix the "Millionaire" bug:
                        cRate = (inputCRate >= 1) ? inputCRate / 100 : inputCRate;
                    }
                    // Calls Overload #3 (Master Method) with 'true' for compound
                    interestEarned = account.CalculateInterest(months, cRate, true);
                    break;
            }

            Console.WriteLine($"\n--- Interest Results ---");
            Console.WriteLine($"Interest Earned: {interestEarned:C}");
            Console.WriteLine($"New Account Balance: {account.Balance:C}");
        }


        static void HandlePaymentSchedule()
        {
            Console.WriteLine("\n--- Loan Payment Schedule Setup ---");

            // 1. Get and validate the number of months
            int months;
            while (true)
            {
                Console.Write("Enter total number of months for the loan: ");
                if (int.TryParse(Console.ReadLine(), out months) && months > 0 && months <= 12) break;
                Console.WriteLine("Invalid entry! Please enter a positive integer between 1 and 12.");

            }

            // 2. Get and validate the monthly payment amount
            decimal payment;
            while (true)
            {
                Console.Write("Enter the fixed monthly payment amount: ");
                if (decimal.TryParse(Console.ReadLine(), out payment) && payment > 0) break;
                Console.WriteLine("Invalid entry! Please enter a positive numerical amount.");
            }

            Console.WriteLine("\n--- STARTING SCHEDULE ---");

            // 3. Call your recursive method from the LoanCalculator class
            LoanCalculator.DisplayPaymentSchedule(months, payment);

            Console.WriteLine("--- END OF SCHEDULE ---\n");
        }

        static void HandleRecursiveCompoundInterest()
        {
            Console.WriteLine("\n--- Compound Interest ---");

            // 1. Get Principal
            Console.Write("Enter initial principal amount: ");
            decimal.TryParse(Console.ReadLine(), out decimal principal);

            // 2. Get Rate
            decimal rate;
            while (true)
            {
                Console.Write("Enter annual interest rate (e.g., 0.05 for 5% or 5 for 5%): ");
                string? input = Console.ReadLine();

                // 1. Check if it's a valid number
                if (decimal.TryParse(input, out decimal inputRate) && inputRate >= 0)
                {
                    // 2. If they typed "5", make it 0.05
                    rate = (inputRate >= 1) ? inputRate / 100 : inputRate;
                    break; // Valid input, exit loop
                }

                Console.WriteLine("Invalid entry! Please enter a positive numerical interest rate.");
            }

            // 3. Get Years
            Console.Write("Enter number of years: ");
            int.TryParse(Console.ReadLine(), out int years);

            // 4. Call the Recursive Method
            // result = Principal * (1 + rate)^years
            decimal finalAmount = LoanCalculator.CompoundInterest(principal, rate, years);

            Console.WriteLine($"\nAfter {years} years, the total value will be: {finalAmount:C}");
            Console.WriteLine($"Total Interest Earned: {(finalAmount - principal):C}");
        }

        static void HandleSumDeposits()
        {
            Console.WriteLine("\n--- Recursive Sum of All Account Balances ---");

            // 1. Convert our list of account balances into a simple array
            decimal[] balances = _accounts.Select(a => a.Balance).ToArray();

            if (balances.Length == 0)
            {
                Console.WriteLine("No accounts found to sum.");
                return;
            }

            // 2. Call the Recursive Method starting at index 0
            decimal totalBankValue = LoanCalculator.SumDeposits(balances, 0);

            Console.WriteLine($"Total number of accounts summed: {balances.Length}");
            Console.WriteLine($"The recursive sum of all balances is: {totalBankValue:C}");
        }


        static void HandleParameterTesting()
        {
            TransactionProcessor processor = new TransactionProcessor();
            decimal testBalance = 1000m;

            Console.WriteLine("\n--- Testing Pass by Value vs Reference ---");
            Console.WriteLine($"Original Starting Balance: {testBalance:C}");

            // 1. Test Pass by Value
            Console.WriteLine("\n1. Calling TryUpdateBalance (Pass by Value)...");
            processor.TryUpdateBalance(testBalance, 500m);
            Console.WriteLine($"After Method Call (in Main): {testBalance:C} (No Change!)");

            // 2. Test Pass by Reference
            Console.WriteLine("\n2. Calling UpdateBalance (Pass by Reference)...");
            processor.UpdateBalance(ref testBalance, 500m);
            Console.WriteLine($"After Method Call (in Main): {testBalance:C} (Changed!)");

            // 3. Test Pass by Out
            Console.WriteLine("\n3. Calling ProcessTransaction (Pass by Out)...");
            if (processor.ProcessTransaction(500m, "Deposit", out string code, out DateTime time))
            {
                Console.WriteLine($"Transaction Successful!");
                Console.WriteLine($"Out Confirmation Code: {code}");
                Console.WriteLine($"Out Timestamp: {time:dd/MM/yyyy HH:mm:ss}");
            }
        }

        static void HandleOptionalParameters()
        {
            AccountManager manager = new AccountManager();

            Console.WriteLine("\n--- Part 5: Optional & Named Parameters Demo ---");

            // 1. Only required parameters (Uses all 3 defaults)
            manager.CreateSavingsAccount("Alice Johnson", 5000m);

            // 2. Some optional parameters (Overrides rate, uses other 2 defaults)
            manager.CreateSavingsAccount("Bob Smith", 3000m, 0.05m);

            // 3. All parameters (No defaults used)
            manager.CreateSavingsAccount("Charlie Davis", 10000m, 0.03m, 500, "North Branch");

            // 4. Named parameters (Shows you can change the order)
            manager.CreateSavingsAccount(branch: "West End", initialDeposit: 2500m, accountHolder: "Diana Prince");

            // 5. Log Transaction with optional parameters
            manager.LogTransaction("Deposit", 500m, category: "Salary", sendEmail: true);
        }




        static void DisplayBankStatistics()
        {
            if (_accounts.Count == 0)
            {
                Console.WriteLine("No statistics available. The bank has no accounts.");
                return;
            }

            // 1. Calculations
            decimal grandTotal = 0;
            decimal maxBalance = _accounts[0].Balance;
            decimal minBalance = _accounts[0].Balance;
            string richestUser = _accounts[0].AccountHolder;

            foreach (var acc in _accounts)
            {
                grandTotal += acc.Balance;

                if (acc.Balance > maxBalance)
                {
                    maxBalance = acc.Balance;
                    richestUser = acc.AccountHolder;
                }

                if (acc.Balance < minBalance)
                {
                    minBalance = acc.Balance;
                }
            }

            decimal average = grandTotal / _accounts.Count;

            Console.WriteLine("\n============================================");
            Console.WriteLine("        GLOBAL BANK ANALYTICS              ");
            Console.WriteLine("============================================");
            Console.WriteLine($"Total Active Accounts : {_accounts.Count}");
            Console.WriteLine($"Grand Total Deposits  : {grandTotal:C}");
            Console.WriteLine($"Average Account Value : {average:C}");
            Console.WriteLine($"Highest Balance       : {maxBalance:C} ({richestUser})");
            Console.WriteLine($"Lowest Balance        : {minBalance:C}");
            Console.WriteLine("--------------------------------------------");


            Console.Write("Apply standard 2% monthly interest to ALL accounts? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                foreach (var acc in _accounts)
                {
                    acc.ApplyInterest(1, 0.02m);
                }
                Console.WriteLine("\n[SUCCESS] Monthly interest applied across the system.");
            }
            Console.WriteLine("============================================\n");
        }


    }
}
