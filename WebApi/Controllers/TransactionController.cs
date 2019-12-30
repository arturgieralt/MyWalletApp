using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWalletApp.WebApi.Commands.AddTransaction;
using MyWalletApp.WebApi.DTO.Requests;

namespace MyWalletApp.WebApi.Controllers
{
    [Route("transactions")]
    [Authorize]
    [ApiController]
    public class TransactionController: Controller
    {
        
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(IMediator mediator, ILogger<TransactionController> logger) 
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TransactionRequest request)
        {

            var result = await _mediator.Send(new AddTransactionCommand(){
                Name = request.Name,
                AccountId = request.AccountId,
                Total = request.Total,
                TransactionType = request.TransactionType,
                CurrencyId = request.CurrencyId,
                CategoryId = request.CategoryId
            });

            return Ok(result);
        }
    }
}