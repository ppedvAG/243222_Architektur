using BeanRider.Model.Contracts.Data;
using BeanRider.Model.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace BeanRider.Data.Db
{

    public class EfContextUnitOfWorkAdapter : IUnitOfWork
    {
        EfContext _context;

        public EfContextUnitOfWorkAdapter(string conString)
        {
            _context = new EfContext(conString);
        }

        public IRepository<Order> OrderRepo => new EfContextRepositoryAdapter<Order>(_context);

        public IRepository<Food> FoodRepo => new EfContextRepositoryAdapter<Food>(_context);    

        public ICustomerRepository CustomerRepo => new EfContextCustomerRepositoryAdapter(_context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }

    public class EfContextCustomerRepositoryAdapter : EfContextRepositoryAdapter<Customer>, ICustomerRepository
    {
        public EfContextCustomerRepositoryAdapter(EfContext efContext) : base(efContext)
        {        }

        public Customer CustomerWithMostUmsatz()
        {
            return _context.Customers.FromSql($"exec sp_BestCustomer").ToList().FirstOrDefault();
        }
    }
    

    public class EfContextRepositoryAdapter<T> : IRepository<T> where T : Entity
    {
        protected EfContext _context;

        public EfContextRepositoryAdapter(EfContext efContext)
        {
            _context = efContext;
        }

        public void Add(T entity)
        {
            //if(typeof(T) == typeof(Customer))
            //    _context.Customers.Add(entity as Customer);

            _context.Add(entity);
        }



        public void Delete(T entity)
        {
            _context.Remove(entity);
        }


        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }



        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>();
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
