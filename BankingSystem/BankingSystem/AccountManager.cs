public class AccountManager
{
    // Part 5: Optional Parameters
    public void CreateSavingsAccount(string accountHolder, decimal initialDeposit, 
        decimal interestRate = 0.02m, int minimumBalance = 1000, string branch = "Main Branch")
    {
        Console.WriteLine($"Account Created for {accountHolder} at {branch}. " +
                          $"Rate: {interestRate:P}, Min Balance: {minimumBalance:C}");
    }

    public void LogTransaction(string transactionType, decimal amount, 
        string description = "No description", bool sendEmail = false, string category = "No category")
    {
        Console.WriteLine($"[{category}] {transactionType}: {amount:C} - {description}");
        if (sendEmail) Console.WriteLine("Notification email sent to user.");
    }
}
