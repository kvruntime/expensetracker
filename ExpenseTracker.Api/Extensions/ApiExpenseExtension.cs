using ExpenseTracker.Api.Data;
using ExpenseTracker.Shared;

namespace ExpenseTracker.Api.Extensions
{
    public static class ApiTaskItemExtension
    {
        public static void MapExpenseEndpoints(this WebApplication app)
        {
            var groups = app.MapGroup("/api/expenses");

            groups.MapGet("/", async (IDbStore store) =>
            {
                var items = await store.GetAll();

                return Results.Ok(items.ToList().Select(item => item.ReadToDto()));
            });

            groups.MapGet("/{id}", async (IDbStore store, string Id) =>
            {
                var item = await store.Get(Id);
                if (item == null) return Results.NotFound();
                return Results.Ok(item.ReadToDto());

            }).WithName("GetExpense");

            groups.MapPost("/", async (IDbStore store, ExpenseCreateDto dto) =>
            {
                var taskitem = Expense.CreateFromDto(dto);
                await store.Add(taskitem);
                await store.Save();
                return Results.Created($"/GetExpense/{taskitem.Id}", taskitem.ReadToDto());

            });
            groups.MapDelete("/{id}", async (IDbStore store, string Id) =>
            {
                var item = await store.Get(Id);
                if (item == null) return Results.NotFound();
                store.Delete(item);
                await store.Save();
                return Results.NoContent();
            });


            groups.MapPut("/{id}", async (IDbStore store, string Id, ExpenseReadDto dto) =>
            {
                var item = await store.Get(Id);
                if (item == null || item.Id != dto.Id)
                    return Results.NotFound();

                // store.Update(Id, item);
                item.UpdateFromDto(dto);
                await store.Save();
                return Results.NoContent();
            });
        }
    }
}