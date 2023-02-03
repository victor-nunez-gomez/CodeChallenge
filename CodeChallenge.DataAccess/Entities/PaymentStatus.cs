using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.DataAccess.Entities;

[Table("PaymentStatus", Schema = "dbo")]
public class PaymentStatus : BaseEntity
{
    [MaxLength(10)]
    public string Status { get; set; } = string.Empty;
}