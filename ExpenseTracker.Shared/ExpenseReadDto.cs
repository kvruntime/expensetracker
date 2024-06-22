using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Shared
{
    public class ExpenseReadDto
    {
        public string? Id { get; set; } = null;
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
        public decimal? Amount { get; set; }
        public bool Paid { get; set; }
    }
}