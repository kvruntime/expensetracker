using ExpenseTracker.Shared;

namespace ExpenseTracker.Api.Data
{
    public interface IDbStore : IEntityRepo<Expense>
    {

    }
}