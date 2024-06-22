using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Shared
{
    public class Entity
    {
        [Required]
        [Key]
        [Column(name:"id")]
        public string? Id { get; set; }
    }
}