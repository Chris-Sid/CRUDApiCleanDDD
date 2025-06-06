using Microsoft.AspNetCore.Mvc;
using MyApiProject.Domain.Entities;
using MyApiProject.Infrastructure.Services.Session;

namespace MyApiProject.API.Controllers.MockCRUD
{
        [Route("api/mock/customers")]
        public class MockCustomersController : ControllerBase
        {
            private readonly ISessionService _cacheService;
            private const string CacheKey = "MockCustomers";

            public MockCustomersController(ISessionService cacheService)
            {
                _cacheService = cacheService;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
            {
                var customers = await _cacheService.GetAsync<List<Customer>>(CacheKey) ?? new List<Customer>();
                return Ok(customers);
            }

            [HttpPost]
            public async Task<IActionResult> Create(Customer customer)
            {
                var customers = await _cacheService.GetAsync<List<Customer>>(CacheKey) ?? new List<Customer>();
                customer.Id = Guid.NewGuid();
                customers.Add(customer);
                await _cacheService.SetAsync(CacheKey, customers);
                return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Customer>> GetById(Guid id)
            {
                var customers = await _cacheService.GetAsync<List<Customer>>(CacheKey);
                var customer = customers?.FirstOrDefault(c => c.Id == id);
                return customer is null ? NotFound() : Ok(customer);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(Guid id,[FromBody] Customer updated)
            {
                var customers = await _cacheService.GetAsync<List<Customer>>(CacheKey);
                if (customers == null) return NotFound();

                var customer = customers.FirstOrDefault(c => c.Id == id);
                if (customer == null) return NotFound();

                customer.name = updated.name;
                customer.email = updated.email;
                customer.phone = updated.phone;
                customer.status = updated.status;
                await _cacheService.SetAsync(CacheKey, customers);

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                var customers = await _cacheService.GetAsync<List<Customer>>(CacheKey);
                if (customers == null) return NotFound();

                var customer = customers.FirstOrDefault(c => c.Id == id);
                if (customer == null) return NotFound();

                customers.Remove(customer);
                await _cacheService.SetAsync(CacheKey, customers);
                return NoContent();
            }
        }
    }
