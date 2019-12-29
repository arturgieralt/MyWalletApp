using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWalletApp.Extensions;
using MyWalletApp.WebApi.Commands.CreateAccount;
using MyWalletApp.WebApi.DTO.Requests;

namespace MyWalletApp.WebApi.Controllers
{
    [Route("accounts")]
    [Authorize]
    [ApiController]
    public class AccountController: Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMediator mediator, ILogger<AccountController> logger) 
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountRequest request)
        {

            var result = await _mediator.Send(new CreateAccountCommand(){
                Name = request.Name,
                CurrencyId = request.CurrencyId
            });

            return Ok(result);
        }
        
    }
}