using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.InviteUser
{
    public class InviteUserCommandHandler: IRequestHandler<InviteUserCommand, CommandResult>
    {
        private readonly IAccountUserInviteRepository _accountUserInviteRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public InviteUserCommandHandler(
            IAccountUserInviteRepository accountUserInviteRepository,
            IAccountRepository accountRepository,
            IApplicationUserRepository applicationUserRepository){

            _accountUserInviteRepository = accountUserInviteRepository;
            _accountRepository = accountRepository;
            _applicationUserRepository = applicationUserRepository;
        }        

        public async Task<CommandResult> Handle (InviteUserCommand request, CancellationToken cancellationToken) {

            var user = await _applicationUserRepository.GetByEmail(request.Email);
            if(user == null) {
                return new CommandResult(){
                    Status = CommandResultStatus.Error,
                    Message = "User with that email does not exist in the database."
                };
            }
            var account = await _accountRepository.GetById(request.AccountId);
            if(account == null) {
                return new CommandResult(){
                    Status = CommandResultStatus.Error,
                    Message = "Wrong account Id."
                };
            }

            var hasAlreadyAccess = await _accountRepository.DoesExistForUser(account.Id, user.Id);

            if(hasAlreadyAccess) {
                return new CommandResult(){
                    Status = CommandResultStatus.Error,
                    Message = "User is already assigned to this account."
                };
            }

            var isInvited = await _accountUserInviteRepository.IsUserAlreadyInvited(user.Id, account.Id);

            if(isInvited) {
                return new CommandResult(){
                    Status = CommandResultStatus.Error,
                    Message = "User has been already invited."
                };
            }

            var accountUserInvite = new AccountUserInvite()
            {
                UserId = user.Id,
                AccountId = account.Id,
                TransactionRead = request.TransactionRead,
                TransactionWrite = request.TransactionWrite,
                AccountDelete = request.AccountDelete,
                AccountWrite = request.AccountWrite
            };

            try {
                var inviteId =  await _accountUserInviteRepository.Save(accountUserInvite);

                return new CommandResult(){Status = CommandResultStatus.Success, Message = "Created"};
            }
            catch (DbUpdateException e) {
                return new CommandResult(){Status = CommandResultStatus.Error, Message = e.Message };
            }

            
        }

    }
}