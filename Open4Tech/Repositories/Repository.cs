using KidsVaccineReminder.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KidsVaccineReminder.Repositories
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private AppDbContext _context;
        private DbSet<T> _entity = null;
        public Repository(AppDbContext context)
        {
            _context = context;
            this._entity = _context.Set<T>();
        }
        public void Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _entity.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> GetAll()
        {
            return await _entity.ToListAsync();
        }

        public void Add(T obj)
        {
            _entity.Add(obj);
        }

        public void Update(T obj)
        {
            _entity.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public async void Save()
        {
           await _context.SaveChangesAsync();
        }
    }
}
