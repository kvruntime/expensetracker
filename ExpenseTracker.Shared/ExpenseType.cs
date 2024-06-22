namespace ExpenseTracker.Shared
{
    public class ExpenseType
    {
        public string Id { get; set; }=Guid.NewGuid().ToString();   
        public string? Type { get; set; }
    }
}