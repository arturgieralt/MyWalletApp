using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.WebApi.Commands.AddCategory;
using MyWalletApp.WebApi.DTO.Requests;
using MyWalletApp.WebApi.DTO.Results;
using MyWalletApp.WebApi.Queries.GetAllCategories;

namespace MyWalletApp.WebApi.Controllers
{
    [Route("api/categories")]
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());
            var response = new ListResult<Category>(){
                Items = result.Categories
            };
            return Ok(response);

        }
    }
}