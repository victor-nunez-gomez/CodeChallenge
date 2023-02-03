using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.DataAccess.Entities;

[Table("Card", Schema = "dbo")]
public class Card : BaseEntity
{
    [Required]
    [StringLength(15)]
    public string CardNumber { get; set; }

    [Required]
    public string CardExpiration { get; set; } = string.Empty;

    [Required]
    public string ClientName { get; set; } = String.Empty;

    public decimal Balance { get; set; }
}
