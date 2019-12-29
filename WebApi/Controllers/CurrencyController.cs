using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.WebApi.DTO.Results;
using MyWalletApp.WebApi.Queries.GetAllCurrencies;

namespace MyWalletApp.WebApi.Controllers
{
    [Route("currencies")]
    [Authorize]
    [ApiController]
    public class CurrencyController: Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(IMediator mediator, ILogger<CurrencyController> logger) 
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCurrenciesQuery());
            var response = new ListResult<Currency>(){
                Items = result.Currencies
            };
            return Ok(response);

        }
        
    }
}