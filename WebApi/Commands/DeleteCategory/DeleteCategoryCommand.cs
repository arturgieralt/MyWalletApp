using MediatR;
using MyWalletApp.WebApi.Commands.Common;

namespace MyWalletApp.WebApi.Commands.DeleteCategory
{
    public class DeleteCategoryCommand: IRequest<CommandResult>
    {
        public long Id {get; set;}
    }
}