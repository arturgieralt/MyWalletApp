using MediatR;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.AddCategory
{
    public class AddCategoryCommand: IRequest<CommandResult>
    {
        public string Name {get; set;}
    }
}