// See https://aka.ms/new-console-template for more information
using System;

namespace BankingSystem
{
    class Program
    {
        private static List<BankAccount> _accounts = new List<BankAccount>();
        static void Main(string[] args)
        {

            // Initializing our classes
            AccountManager manager = new AccountManager();
            TransactionProcessor processor = new TransactionProcessor();

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

                        break;
                    case 9:
                        //Calculate Compound Interest (Recursive

                        break;
                    case 10:
                        //Sum Deposit Array (Recursive

                        break;

                    case 11:
                        //Test Pass by Value/Reference
                        break;

                    case 12:
                        //Display Bank Statistics
                        BankAccount.DisplayBankStatistics();


                        break;
                    case 13:
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
            Console.WriteLine("6. Transfer Money (Overload)");
            Console.WriteLine("7. Calculate Interest");
            Console.WriteLine("8. Display Payment Schedule (Recursive)");
            Console.WriteLine("9. Calculate Compound Interest (Recursive)");
            Console.WriteLine("10. Sum Deposit Array (Recursive)");
            Console.WriteLine("11. Test Pass by Value/Reference");
            Console.WriteLine("12. Display Bank Statistics");
            Console.WriteLine("13. Exit");
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

    }
}
