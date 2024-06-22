using System.Linq.Expressions;

namespace Shared.Domain
{
    public interface IRepository<Tkey, T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllBy(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAllQuery();
        IQueryable<T> GetAllByQuery(Expression<Func<T, bool>> expression);
        T GetById(Tkey id);
        Task<T> GetByIdAsync(Tkey id);
        bool Create(T Entity);
        bool Delete(T Entity);
        bool ExistBy(Expression<Func<T, bool>> expression);
        bool Save();
        Task<bool> SaveAsync();
        Task<bool> CreateAsync(T Entity);
    }
}
