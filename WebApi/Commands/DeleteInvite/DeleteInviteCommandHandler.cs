using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.WebApi.Commands.Common;
using MyWalletApp.Services.Providers;
using MyWalletApp.Extensions;


namespace MyWalletApp.WebApi.Commands.DeleteInvite
{
    public class DeleteInviteCommandHandler: IRequestHandler<DeleteInviteCommand, CommandResult>
    {
        private readonly IAccountUserInviteRepository _accountUserInviteRepository;
        private readonly IUserContextProvider _userContextProvider;

        public DeleteInviteCommandHandler(
            IAccountUserInviteRepository accountUserInviteRepository,
            IUserContextProvider userContextProvider
            ){

            _accountUserInviteRepository = accountUserInviteRepository;
            _userContextProvider = userContextProvider;
        }        

        public async Task<CommandResult> Handle (DeleteInviteCommand request, CancellationToken cancellationToken) {

            var userId = _userContextProvider.GetUser.GetId<string>();
            var invite = await _accountUserInviteRepository.GetByIdForUserOrCreator(request.Id, userId);

            if(invite == null) {
                return ErrorResult("Wrong invite id.");
            }

            try {
                await _accountUserInviteRepository.Delete(invite);

                return new CommandResult(){Status = CommandResultStatus.Success, Message = "Deleted"};
            }
            catch (DbUpdateException e) {
                return ErrorResult(e.Message);
            }

            
        }

        private CommandResult ErrorResult(string message) => new CommandResult()
                    {
                        Status = CommandResultStatus.Error, 
                        Message = message
                    };

    }
}