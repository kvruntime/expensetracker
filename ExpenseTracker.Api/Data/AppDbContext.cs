using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Shared;

namespace ExpenseTracker.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseType> ExpenseType { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}