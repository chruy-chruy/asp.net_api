// Controllers/CustomerController.cs

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly SampleDBContext _context;
        public CustomerController(SampleDBContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers()
        {
            return _context.Customer.ToList();
        }

        // GET: api/Customer/1
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _context.Customer.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        // POST: api/Customer
        [HttpPost]
        public ActionResult<Customer> CreateCustomer(Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            _context.Customer.Add(customer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);
        }

        // Put: api/Customer/id
        [HttpPut("{id}")]
        public ActionResult<Customer> Update(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            var existing = _context.Customer.Find(id);
            if (existing == null)
            {
                return NotFound();
            }

            _context.Entry(existing).State = EntityState.Detached;
            _context.Customer.Update(customer);
            _context.SaveChanges();

            return customer;
        }

        // Delete: api/Customer/1
        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete(int id)
        {
            var customer = _context.Customer.Find(id);
            if (customer == null)
            {
                return BadRequest();
            }


            _context.Customer.Remove(customer);
            _context.SaveChanges();

            return NoContent();
        }

    }
}