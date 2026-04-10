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
                            // Display the table header
                            Console.WriteLine($"\n{"Account Number",-15} | {"Account Holder",-20} | {"Balance",15}");
                            Console.WriteLine(new string('-', 55));
                            // Display the data
                            Console.WriteLine($"{acc.AccountNumber,-15} | {acc.AccountHolder,-20} | {acc.Balance,15:C}");
                        }
                        break;
                        case 5:

                        break;
                    case 6:
                        //Display all Account Info
                        DisplayAccounts();
                        break;
                    case 7:

                        break;
                    case 8:

                        break;
                    case 9:

                        break;
                    case 10:
                        decimal tempBal = 100m;
                        processor.TryUpdateBalance(tempBal, 50m); // Value
                        Console.WriteLine($"After Value call: {tempBal:C}");
                        processor.UpdateBalance(ref tempBal, 50m); // Ref
                        Console.WriteLine($"After Ref call: {tempBal:C}");
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
            Console.WriteLine("4. Transfer Money (Overload)");
            Console.WriteLine("5. Calculate Interest");
            Console.WriteLine("6. View Account Info");
            Console.WriteLine("7. Display Payment Schedule (Recursive)");
            Console.WriteLine("8. Calculate Compound Interest (Recursive)");
            Console.WriteLine("9. Sum Deposit Array (Recursive)");
            Console.WriteLine("10. Test Pass by Value/Reference");
            Console.WriteLine("11. Display Bank Statistics");
            Console.WriteLine("12. Exit");
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


    }
}
