using CodeChallenge.DataAccess.Entities;

namespace CodeChallenge.DataAccess.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    IEnumerable<T> GetAll();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
}