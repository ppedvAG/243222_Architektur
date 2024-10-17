using BeanRider.Model.Contracts.Data;
using BeanRider.Model.DomainModel;
using BeanRider.UI.Api.Web.Mapper;
using BeanRider.UI.Api.Web.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BeanRider.UI.Api.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository repo;

        public OrdersController(IRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<OrderDTO> Get()
        {
            return repo.GetAll<Order>().Select(x => OrderMapper.ToDTO(x));
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public OrderDTO Get(int id)
        {
            return OrderMapper.ToDTO(repo.GetById<Order>(id));
        }

        // POST api/<OrdersController>
        [HttpPost]
        public void Post([FromBody] OrderDTO value)
        {
            var customer = repo.GetById<Customer>(value.CustomerId);
            value.Status= "New";
            var order = OrderMapper.ToEntity(value, customer);
            order.Status = OrderStatus.New;
            repo.Add(order);
            repo.SaveChanges();
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] OrderDTO value)
        {
            var customer = repo.Query<Customer>().FirstOrDefault(x => x.Id == value.CustomerId);
            var order = OrderMapper.ToEntity(value, customer);
            repo.Update(order);
            repo.SaveChanges();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var loaded =  repo.GetById<Order>(id);
            repo.Delete(loaded);
            repo.SaveChanges();
        }
    }
}
