using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWalletApp.WebApi.Commands.AddTransaction;
using MyWalletApp.WebApi.DTO.Requests;
using MyWalletApp.WebApi.DTO.Results;
using MyWalletApp.WebApi.Queries.GetAllTransactions;
using Transaction = MyWalletApp.WebApi.Models.Transaction;

namespace MyWalletApp.WebApi.Controllers
{
    [Route("api/transactions")]
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
                Date = request.Date,
                AccountId = request.AccountId,
                Total = request.Total,
                TransactionType = request.TransactionType,
                CategoryId = request.CategoryId
            });

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetTransactionsRequest request)
        {
            var result = await _mediator.Send(new GetAllTransactionsQuery()
            {
                AccountId = request.AccountId
            });
            var response = new ListResult<Transaction>(){
                Items = result.Transactions
            };
            return Ok(response);

        }
    }
}