using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Shared;

namespace ExpenseTracker.Api.Data
{
    public class DbStore : IDbStore
    {
        private readonly AppDbContext _context;

        public DbStore(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Expense item)
        {
            await _context.Expenses.AddAsync(item);
        }

        public void Delete(Expense item)
        {
            _context.Remove(item);
        }

        public async Task<Expense>? Get(string Id)
        {
            var item = await _context.Expenses.FirstOrDefaultAsync(item => item.Id == Id);
            return item;
        }

        public async Task<IList<Expense>> GetAll()
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(string Id, Expense item)
        {
            if (await _context.Expenses.FirstOrDefaultAsync(item => item.Id == Id) is not null)
                _context.Update(item);
        }
    }
}