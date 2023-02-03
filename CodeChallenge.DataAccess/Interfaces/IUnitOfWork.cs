using CodeChallenge.DataAccess.Entities;

namespace CodeChallenge.DataAccess.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<Card> CardRepository { get; }

    IBaseRepository<Payment> paymentRepository { get; }

    void SaveChanges();

    Task SaveChangesAsync();
}
