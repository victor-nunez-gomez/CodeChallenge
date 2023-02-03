using Microsoft.EntityFrameworkCore;
using CodeChallenge.DataAccess.Entities;

namespace CodeChallenge.DataAccess.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Card> Cards { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<PaymentStatus> PaymentsStatus { get; set; }

}
