using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.WebApi.Commands.Common;
using MyWalletApp.Services.Providers;
using MyWalletApp.Extensions;


namespace MyWalletApp.WebApi.Commands.InviteUser
{
    public class InviteUserCommandHandler: IRequestHandler<InviteUserCommand, CommandResult>
    {
        private readonly IAccountUserInviteRepository _accountUserInviteRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IUserContextProvider _userContextProvider;

        public InviteUserCommandHandler(
            IAccountUserInviteRepository accountUserInviteRepository,
            IAccountRepository accountRepository,
            IApplicationUserRepository applicationUserRepository,
            IUserContextProvider userContextProvider
            ){

            _accountUserInviteRepository = accountUserInviteRepository;
            _accountRepository = accountRepository;
            _applicationUserRepository = applicationUserRepository;
            _userContextProvider = userContextProvider;
        }        

        public async Task<CommandResult> Handle (InviteUserCommand request, CancellationToken cancellationToken) {

            var user = await _applicationUserRepository.GetByEmail(request.Email);
            if(user == null) {
                return ErrorResult("User with that email does not exist in the database.");
            }

            var httpContextUserId = _userContextProvider.GetUser.GetId<string>();
           
           if(user.Id == httpContextUserId) {
               return ErrorResult("You cannot invite yourself.");
           }
           
            var account = await _accountRepository.GetById(request.AccountId);
            if(account == null) {
                return ErrorResult("Wrong account Id.");
            }

            var hasAlreadyAccess = await _accountRepository.DoesExistForUser(account.Id, user.Id);

            if(hasAlreadyAccess) {
                return ErrorResult("User is already assigned to this account.");
            }

            var isInvited = await _accountUserInviteRepository.IsUserAlreadyInvited(user.Id, account.Id);

            if(isInvited) {
                return ErrorResult("User has been already invited.");
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