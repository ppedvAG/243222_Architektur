using BeanRider.Model.Contracts.Data;
using BeanRider.Model.DomainModel;

namespace BeanRider.Data.Db
{
    public class EfContextRepositoryAdapter : IRepository
    {
        EfContext _context;

        public EfContextRepositoryAdapter(string conString)
        {
            _context = new EfContext(conString);
        }

        public void Add<T>(T entity) where T : Entity
        {
            //if(typeof(T) == typeof(Customer))
            //    _context.Customers.Add(entity as Customer);

            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _context.Remove(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return _context.Set<T>().ToList();
        }

        public T? GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().Find(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Update(entity);
        }
    }
}
