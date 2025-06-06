using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using MyApiProject.Application.DTOs;
using MyApiProject.Application.Helpers;
using MyApiProject.Domain.Entities;
using MyApiProject.Infrastructure.Exceptions;
using MyApiProject.Infrastructure.Logging;
using MyApiProject.Infrastructure.Services.Address;
using MyApiProject.Infrastructure.Services.Session;

namespace MyApiProject.API.Controllers
{
    [Route("api/dms/addresses/")]
    public class AddressController : ControllerBase
    {
        private readonly IAppLogger<AddressService> _logger;
        private readonly ISessionService _sessionStore;
        private readonly IConfiguration _configuration;
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService, IConfiguration configuration, ISessionService sessionStore, IAppLogger<AddressService> logger)
        {
            _addressService = addressService;
            _configuration = configuration;
            _sessionStore = sessionStore;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("{addressId}")]
        public async Task<IActionResult> GetAddress(DmsIdentifier Address, [ModelBinder(BinderType = typeof(AddressRequestHeadersBinder))] AddressRequestHeaders headers, CancellationToken cancellationToken)
        {
            try
            {
                //var headers = new AddressRequestHeaders
                //{
                //    Authorization = Request.Headers["X-Authorization"],
                //    RequestId = Request.Headers["X-RequestId"],
                //    AcceptLanguage = Request.Headers["Accept-Language"],
                //    EntityOwnerUserId = Request.Headers["X-EntityOwnerUserId"],
                //    EntityOwnerCompanyId = Request.Headers["X-EntityOwnerCompanyId"],
                //    DimensionCompany = Request.Headers["X-DimensionCompany"],
                //    DimensionLocation = Request.Headers["X-DimensionLocation"],
                //    DimensionBranch = Request.Headers["X-DimensionBranch"],
                //    DimensionMake = Request.Headers["X-DimensionMake"],
                //    DimensionMarketSegment = Request.Headers["X-DimensionMarketSegment"],
                //    TimezoneOffset = Request.Headers["X-TimezoneOffset"].FirstOrDefault() ?? "0"
                //};
                var address = await _addressService.GetAddressAsync(Address, headers,cancellationToken);
                return Ok(address);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAddress([FromBody] AddressDto Address, [ModelBinder(BinderType = typeof(AddressRequestHeadersBinder))] AddressRequestHeaders headers, CancellationToken cancellationToken)
        {
            try
            {
                var addressDTO = await _addressService.PostAddressAsync(Address, headers, cancellationToken);
                return Ok(addressDTO);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}
