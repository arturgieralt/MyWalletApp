using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.DeleteAccount
{
    public class DeleteAccountCommandHandler: IRequestHandler<DeleteAccountCommand, CommandResult>
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CommandResult> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetById(request.Id);
            await _accountRepository.Delete(account);

            return new CommandResult(){Status = CommandResultStatus.Success, Message = "Deleted"};
        }
    }
}