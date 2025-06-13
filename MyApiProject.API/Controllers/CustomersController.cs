using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApiProject.Application.Interfaces;
using MyApiProject.Domain.Entities.Customers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerService;
    public CustomersController(ICustomerRepository customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        var customers = await _customerService.GetCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(Guid id)
    {
        var customer = await _customerService.GetCustomerAsync(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var customer = await _customerService.CreateCustomerAsync(dto);
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] CustomerUpdateDto dto)
    {
        var updated = await _customerService.UpdateCustomerAsync(id, dto);
        if (!updated)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        var deleted = await _customerService.DeleteCustomerAsync(id);
        if (!deleted)
            return NotFound();
        return NoContent();
    }

    [HttpPost("batch")]
    public async Task<IActionResult> CreateBatch([FromBody] CustomerBatchCreateDto dto)
    {
        foreach (var customerDto in dto.Customers)
        {
            await _customerService.CreateCustomerAsync(customerDto);
        }
        return Ok(new { message = $"{dto.Customers.Count} customers created." });
    }

}