using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KidsVaccineReminder.Repositories
{
    public interface IRepository<T>
    {
        void Add(T obj);
        void Delete(T obj);
        void Update(T obj);
        Task<List<T>> GetAll();
        Task<T> Get(Expression<Func<T, bool>> predicate);
        void Save();
    }
}
