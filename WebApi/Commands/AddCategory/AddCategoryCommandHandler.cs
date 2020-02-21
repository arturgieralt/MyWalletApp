using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.DomainModel.Repositories;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.AddCategory
{

    public class AddCategoryCommandHandler: IRequestHandler<AddCategoryCommand, CommandResult>
    {
        private readonly ICategoryRepository _categoryRepository;

        public AddCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CommandResult> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var doesNameExist = await _categoryRepository.DoesNameExist(request.Name);

            if(doesNameExist) {
                return new CommandResult(){ Status = CommandResultStatus.Error, Message="Name exists." };
            }

            var category = new Category(){
                Name = request.Name
            };

            var categoryId =  await _categoryRepository.Save(category);

            return new CommandResult(){Status = CommandResultStatus.Success, Message = "Created"};
        }
    }
}