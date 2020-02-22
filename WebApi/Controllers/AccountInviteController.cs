using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWalletApp.WebApi.Commands.AcceptInvite;
using MyWalletApp.WebApi.Commands.InviteUser;
using MyWalletApp.WebApi.DTO.Requests;
using MyWalletApp.WebApi.DTO.Results;
using MyWalletApp.WebApi.Queries.GetAllAccountInvites;

namespace MyWalletApp.WebApi.Controllers
{
    [Route("api/invites")]
    [Authorize]
    [ApiController]
    public class AccountInviteController: Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountInviteController> _logger;

        public AccountInviteController(IMediator mediator, ILogger<AccountInviteController> logger) 
        {
            _mediator = mediator;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllAccountInvitesQuery());
            var response = new ListResult<AccountInviteResult>(){
                Items = result.AccountInvites
            };
            return Ok(response);

        }

        [HttpPut] 
        [Route("{inviteId}/accept")]        
        public async Task<IActionResult> Accept(long inviteId) 
        {
            var result = await _mediator.Send(new AcceptInviteCommand(){
                Id = inviteId
            });

            return Ok(result);
        }

        [HttpPost] 
        public async Task<IActionResult> Invite([FromBody] UserInviteRequest request) 
        {
            var result = await _mediator.Send(new InviteUserCommand(){
                Email = request.Email,
                AccountId = request.AccountId,
                TransactionWrite = request.TransactionRead,
                TransactionRead = request.TransactionRead,
                AccountWrite = request.AccountWrite,
                AccountDelete = request.AccountDelete
            });

            return Ok(result);
        }
        
    }
}