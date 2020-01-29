using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.DeleteTransaction
{
    public class DeleteTransactionCommandHandler: IRequestHandler<DeleteTransactionCommand, CommandResult>
    {
        private readonly ITransactionRepository _transactionRepository;

        public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<CommandResult> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _transactionRepository.GetById(request.Id);
            await _transactionRepository.Delete(transaction);

            return new CommandResult(){Status = CommandResultStatus.Success, Message = "Deleted"};
        }
    }
}