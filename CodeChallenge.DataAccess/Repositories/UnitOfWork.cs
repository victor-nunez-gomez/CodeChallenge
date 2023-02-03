using CodeChallenge.DataAccess.Context;
using CodeChallenge.DataAccess.Entities;
using CodeChallenge.DataAccess.Interfaces;

namespace CodeChallenge.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    private readonly IBaseRepository<Card> _cardRepository;
    private readonly IBaseRepository<Payment> _paymentRepository;

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }

    public IBaseRepository<Card> CardRepository => _cardRepository ?? new BaseRepository<Card>(_context);

    public IBaseRepository<Payment> paymentRepository => _paymentRepository ?? new BaseRepository<Payment>(_context);

    public void Dispose()
    {
        if (_context != null)
        {
            _context.Dispose();
        }
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

}
