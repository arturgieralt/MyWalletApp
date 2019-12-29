using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWalletApp.WebApi.Commands.AddCategory;
using MyWalletApp.WebApi.DTO.Requests;

namespace MyWalletApp.WebApi.Controllers
{
    [Route("categories")]
    [Authorize]
    [ApiController]
    public class CategoryController: Controller
    {
        
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IMediator mediator, ILogger<CategoryController> logger) 
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryRequest request)
        {

            var result = await _mediator.Send(new AddCategoryCommand(){
                Name = request.Name,
            });

            return Ok(result);
        }
    }
}