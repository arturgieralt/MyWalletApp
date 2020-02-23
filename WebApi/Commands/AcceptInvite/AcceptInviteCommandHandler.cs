using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.Extensions;
using MyWalletApp.Services.Providers;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.AcceptInvite
{
    public class AcceptInviteCommandHandler: IRequestHandler<AcceptInviteCommand, CommandResult>
    {
        private readonly IAccountUserInviteRepository _accountUserInviteRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IUserContextProvider _userContextProvider;

        public AcceptInviteCommandHandler(
            IAccountUserInviteRepository accountUserInviteRepository,
            IAccountRepository accountRepository,
            IApplicationUserRepository applicationUserRepository,
            IUserContextProvider userContextProvider){

            _accountUserInviteRepository = accountUserInviteRepository;
            _accountRepository = accountRepository;
            _applicationUserRepository = applicationUserRepository;
            _userContextProvider = userContextProvider;
        }

        public async Task<CommandResult> Handle(AcceptInviteCommand request, CancellationToken cancellationToken)
        {

            var userId = _userContextProvider.GetUser.GetId<string>();

            var invite = await _accountUserInviteRepository.GetByIdForUser(request.Id, userId);
            if(invite == null) {
                return ErrorResult("Wrong invite id.");
            }

            var doesAccountExistInOwnerScope = await _accountRepository.DoesExistForUser(invite.AccountId, invite.CreatedById);
            
            if(!doesAccountExistInOwnerScope) {
                return ErrorResult( $"Account with id {invite.AccountId} does not exist anymore.");
            }

            var doesAccountExistInUserScope = await _accountRepository.DoesExistForUser(invite.AccountId, invite.UserId);
            
            if(doesAccountExistInUserScope) {
                return ErrorResult("You are already assigned to this account");
            }

            try {

                var account = await _accountRepository.GetByIdWithoutUserContext(invite.AccountId);
                account.AccountUsers.Add(new AccountUser(
                    invite.UserId, 
                    invite.AccountId,
                    invite.TransactionRead,
                    invite.TransactionWrite,
                    invite.AccountDelete,
                    invite.AccountWrite
                    ));
                await _accountRepository.Save(account);
                await _accountUserInviteRepository.Delete(invite);

                return new CommandResult(){Status = CommandResultStatus.Success, Message = "Created"};
            }
            catch (DbUpdateException e)
            {
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