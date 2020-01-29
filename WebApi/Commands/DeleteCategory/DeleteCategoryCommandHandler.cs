using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler: IRequestHandler<DeleteCategoryCommand, CommandResult>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CommandResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetById(request.Id);
            await _categoryRepository.Delete(category);

            return new CommandResult(){Status = CommandResultStatus.Success, Message = "Deleted"};
        }
    }
}