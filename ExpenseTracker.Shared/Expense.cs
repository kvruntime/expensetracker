using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Shared;

public class Expense : Entity
{
    [Required]
    public DateTime? Date { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Vendor { get; set; }
    public string? Description { get; set; }
    [Required]
    [Display(Name = "Expense Type")]
    public string? ExpenseTypeId { get; set; }
    [Required]
    [Range(0, 500, ErrorMessage = "The {0} field must be <= {2}")]
    public decimal? Amount { get; set; }
    public bool Paid { get; set; }

    public static Expense CreateFromDto(ExpenseCreateDto dto)
    {
        return new Expense()
        {
            Id = Guid.NewGuid().ToString(),
            Date = dto.Date,
            Vendor = dto.Vendor,
            Description = dto.Description,
            ExpenseTypeId = dto.ExpenseTypeId,
            Amount = dto.Amount,
            Paid = dto.Paid,
        };
    }

    public ExpenseReadDto ReadToDto()
    {
        return new ExpenseReadDto()
        {
            Id = this.Id,
            Date = this.Date,
            Vendor = this.Vendor,
            Description = this.Description,
            ExpenseTypeId = this.ExpenseTypeId,
            Amount = this.Amount,
            Paid = Paid,
        };
    }
    public void UpdateFromDto(ExpenseReadDto dto)
    {
        this.Date = dto.Date;
        this.Vendor = dto.Vendor;
        this.Description = dto.Description;
        this.ExpenseTypeId = dto.ExpenseTypeId;
        this.Amount = dto.Amount;
        Paid = dto.Paid;
    }
}