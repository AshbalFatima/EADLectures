using Apis.Common;
using Apis.Common.Models;
using Microsoft.AspNetCore.Mvc;
using VS_APis.DTOs;

namespace VS_APis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //localhost:202/api/Customer/
    public class CustomerController : Controller
    {
        private readonly IRepository<Customer> _CustomerRepo;

        public CustomerController(IRepository<Customer> customerRepo)
        {
            _CustomerRepo = customerRepo;
        }

        [HttpGet]

        public IEnumerable<Customer> GetAll()
        {
            return _CustomerRepo.GetAll();

        }

        [HttpGet("{id}")]

        public Customer GetId(Guid id)
        {
            return _CustomerRepo.GetById(id);
        }
        [HttpPost]
        public void Create(CreateCustomer c)
        {
            var customer = new Customer{
            Id = Guid.NewGuid(),
                Name = c.name,
            Email = c.email,
            ContactNumber=c.contactNumber,
            IsActive =true,
            };
            _CustomerRepo.Create(customer);

        }
        [HttpPut]
        public void Update(UpdateCustomer c)
        {
            var customer = new Customer
            {
                Id = c.id,
                Name = c.name,
                Email = c.email,
                ContactNumber = c.contactNumber,
                IsActive = c.IsActive,
            };
            _CustomerRepo.Update    (customer);

        }
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
         
            _CustomerRepo.Delete(id);
            
        }
    }
}
