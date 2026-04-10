using System.Data;
using System.Security.Cryptography.X509Certificates;

public class BankAccount
{
    //Static variables shared across
    private static int _totalAccounts = 0;
    private static decimal _bankTotalMoney = 0;
    private static int _transactionCounter = 0;
    private static long _lastAccountNumber = 3000000000;

    //Intance variables unique to each account
    public long AccountNumber { get; private set; }
    public  string AccountHolder { get; set; }
    private decimal _balance;
    public string AccountType { get; set; } // "Savings" or "Current"
    public List<string> TransactionHistory { get; private set; } = new List<string>();


    public decimal Balance
    {
        get => _balance;

        set
        {
            if (value > 0)
            {
                _balance = value;
            }
            else
            {
                Console.WriteLine("Enter a valid amount");
            }
        }
    }

    //Constructor
    public BankAccount(string accountHolder, string accountType, decimal initialDeposit)
    {
        this.AccountHolder = accountHolder;
        this.AccountType = accountType;
        this.Balance = initialDeposit;

        // Increment the global counters
        _lastAccountNumber++;
        _totalAccounts++;
        _transactionCounter++;
        _bankTotalMoney += initialDeposit;

        // Assign the unique 10-digit number to this account
        this.AccountNumber = _lastAccountNumber;

        TransactionHistory.Add($"Account opened with {initialDeposit:C} | dd.MM.yyyy HH:mm:ss");
    }

    // Instance Methods

    public bool Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive.");

        _balance += amount;
        _bankTotalMoney += amount;
        _transactionCounter++;

        // Added DateTime.Now to make the timestamp real
        TransactionHistory.Add($"Deposit: +{amount:C}. New Balance: {_balance:C} | {DateTime.Now:dd.MM.yyyy HH:mm:ss}");

        return true;
    }

    public bool Withdrawal(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal must be positive.");

        if (amount > _balance)
            throw new InvalidOperationException("Insufficient funds.");

        _balance -= amount;
        _bankTotalMoney -= amount;
        _transactionCounter++;

      
        TransactionHistory.Add($"Withdrawal: -{amount:C}. New Balance: {_balance:C} | {DateTime.Now:dd.MM.yyyy HH:mm:ss}");

        return true;
    }
   public void DisplayAccountInfo()
{
    // Table Header
    Console.WriteLine($"\n{"Account Number",-15} | {"Account Holder",-20} | {"Account Type",-12} | {"Balance (£)",15} | {"Date & Time",-15}");
    Console.WriteLine(new string('-', 90));

    // Table Row (using the instance's own data)
    Console.WriteLine($"{AccountNumber,-15} | {AccountHolder,-20} | {AccountType,-12} | {_balance,15:C} | {DateTime.Now,-15:dd MMM yyyy}");
}

    public decimal GetBalance()
    {
        return _balance;
    }

    // tatic Methods 

    public static int GetTotalAccounts()
    {
        return _totalAccounts;
    }

    public static decimal GetBankTotalMoney()
    {
        return _bankTotalMoney;
    }

    public static int GetTotalTransactions()
    {
        return _transactionCounter;
    }

    public static void DisplayBankStatistics()
    {
        Console.WriteLine("\n========================================");
        Console.WriteLine("       BANK STATISTICS           ");
        Console.WriteLine("========================================");
        Console.WriteLine($"Total Accounts Opened:    {_totalAccounts}");
        Console.WriteLine($"Total System Transactions: {_transactionCounter}");
        Console.WriteLine($"Total Money in the Bank System:   {_bankTotalMoney:C}");
        Console.WriteLine("========================================\n");
    }




    // Overloaded Transfer Methods

    // 1. Simple Transfer (Amount only)
    public bool Transfer(BankAccount destinationAccount, decimal amount)
    {

        return Transfer(destinationAccount, amount, "Standard Transfer", false);
    }

    // 2. Transfer with Description
    public bool Transfer(BankAccount destinationAccount, decimal amount, string description)
    {
        // Pass the custom description, but keep notification false
        return Transfer(destinationAccount, amount, description, false);
    }

    // 3. Full Transfer (Amount, Description, and Notification)
    public bool Transfer(BankAccount destinationAccount, decimal amount, string description, bool sendNotification)
    {
        // 1. Validation (Always check if you actually HAVE the money first!)
        if (destinationAccount == null) return false;
        if (amount <= 0 || amount > _balance)
        {
            Console.WriteLine("Transfer failed: Insufficient funds or invalid amount.");
            return false;
        }

        // 2. The Exchange (The money leaves your account and enters theirs)
        this.Withdrawal(amount);               // 'this' is the Sender
        destinationAccount.Deposit(amount);  // 'destinationAccount' is the Receiver

        // 3. Notification Logic (Now handles BOTH sides)
        if (sendNotification)
        {
            // Notification for the Sender (You)
            Console.WriteLine($"[ALERT] {this.AccountHolder}: You sent {amount:C} to {destinationAccount.AccountHolder}.");

            // Notification for the Receiver
            Console.WriteLine($"[ALERT] {destinationAccount.AccountHolder}: You received {amount:C} from {this.AccountHolder}.");
        }

        return true;
    }


    // Overloaded Calculate Interest Methods 

    // 1. Simple Interest (Default rate of 2% per month)
    public decimal CalculateInterest(int months)
    {
        // Simplified version calling the custom rate version
        return CalculateInterest(months, 0.02m);
    }

    // 2. Interest with Custom Rate (Simple Interest)
    public decimal CalculateInterest(int months, decimal customRate)
    {
        // Calling the "master" method with compound set to false
        return CalculateInterest(months, customRate, false);
    }

    // 3. Master Method (Handles Simple vs Compound)
    public decimal CalculateInterest(int months, decimal customRate, bool compound)
    {
        if (months <= 0 || customRate < 0) return 0;

        if (compound)
        {
            // Formula: balance * (1 + rate)^months - balance
            // We use (double) because Math.Pow requires doubles
            double baseVal = 1 + (double)customRate;
            double result = (double)_balance * Math.Pow(baseVal, months);

            // Subtract original balance to get only the INTEREST earned
            return (decimal)result - _balance;
        }
        else
        {
            // Simple interest: balance * rate * months
            return _balance * customRate * months;
        }
    }


}