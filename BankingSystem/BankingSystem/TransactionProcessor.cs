public class TransactionProcessor
{
    // Pass by Value
    public void TryUpdateBalance(decimal balance, decimal amount)
    {
        // This only modifies the local copy of the balance
        balance += amount; 

        Console.WriteLine($"Inside TryUpdateBalance (local copy): {balance:C}");
    }

    // Pass by Reference
public void UpdateBalance(ref decimal balance, decimal amount)
{
    // Because of 'ref', this directly changes the variable 
    balance += amount; 
}

public bool ProcessTransaction(decimal amount, string type, out string confirmationCode, out DateTime timestamp)
{
    // Validation: If the amount is zero or negative, we fail
    if (amount <= 0)
    {
        confirmationCode = "INVALID";
        timestamp = DateTime.Now;
        return false;
    }

    // Create a unique code (e.g., DEP-12345)
    string uniqueId = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
    confirmationCode = $"{type.ToUpper().Substring(0, 3)}-{uniqueId}";

    // Set the timestamp
    timestamp = DateTime.Now;

    //Return success
    return true;
}


}
