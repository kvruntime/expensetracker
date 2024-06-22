using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Shared;

public class ExpenseCreateDto
{
    [Required]
    public DateTime? Date { get; set; } = DateTime.Now;
    [Required]
    [MaxLength(100)]
    public string? Vendor { get; set; }
    public string? Description { get; set; }
    [Required]
    [Display(Name = "Expense Type")]
    public string? ExpenseTypeId { get; set; }
    [Required]
    [Range(0, 500, ErrorMessage = "The {0} field must be <= {2}")]
    [DefaultValue(5)]
    public decimal? Amount { get; set; }
    [DefaultValue(false)]
    public bool Paid { get; set; } = false;

    public static ExpenseCreateDto CreateFromReadDto(ExpenseReadDto dto)
    {
        return new ExpenseCreateDto()
        {
            Date = dto.Date,
            Vendor = dto.Vendor,
            Description = dto.Description,
            ExpenseTypeId = dto.ExpenseTypeId,
            Paid = dto.Paid,
            Amount = dto.Amount,
        };


    }
}