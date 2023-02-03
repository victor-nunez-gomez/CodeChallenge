using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.DataAccess.Entities;

[Table("Payment", Schema = "dbo")]
public class Payment : BaseEntity
{

    [Required]
    public int CardId { get; set; }

    public DateTimeOffset TransactionDate { get; set; } = DateTimeOffset.UtcNow;

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public decimal Fee { get; set; }

    [Required]
    public PaymentStatus PaymentStatus { get; set; }
}