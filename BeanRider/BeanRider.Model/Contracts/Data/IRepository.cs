using BeanRider.Model.DomainModel;

namespace BeanRider.Model.Contracts.Data
{


    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Order> OrderRepo { get; }
        public IRepository<Food> FoodRepo { get; }
        public ICustomerRepository CustomerRepo { get; }


        void SaveChanges();
    }

    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer CustomerWithMostUmsatz();

    }

    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        IQueryable<T> Query();
        T? GetById(int id);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
