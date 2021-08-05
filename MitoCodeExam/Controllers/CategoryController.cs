using Microsoft.AspNetCore.Mvc;
using MitoCodeExam.Dto.Request;
using MitoCodeExam.Dto.Response;
using MitoCodeExam.Entities;
using MitoCodeExam.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace MitoCodeExam.Controllers
{
   [ApiController]
   [ApiVersion(Constants.V1)]
   [Route(Constants.RouteTemplate)]
   public class CategoryController : ControllerBase
   {
      private readonly ICategoryService _service;

      public CategoryController(ICategoryService service)
      {
         _service = service;
      }

      [HttpGet]
      [SwaggerResponse(Constants.Ok, Constants.ReadyMessage, typeof(CategoryDtoResponse))]
      public async Task<IActionResult> Get([FromQuery] string filter, int page = 1, int rows = 4)
      {
         return Ok(await _service.GetCollectionAsync(new BaseDtoRequest(filter, page, rows)));
      }

      [HttpGet]
      [Route("{id:int}")]
      [SwaggerOperation()]
      [SwaggerResponse(Constants.Ok, Description = Constants.ReadyMessage, Type = typeof(ResponseDto<CategoryDtoRequest>))]
      [SwaggerResponse(Constants.NotFound, Description = Constants.NotFoundMessage, Type = typeof(ResponseDto<CategoryDtoRequest>))]
      public async Task<IActionResult> Get(int id)
      {
         var response = await _service.GetAsync(id);

         return response.Success ? Ok(response) : NotFound();
      }

      [HttpPost]
      [SwaggerResponse(Constants.Created, Constants.CreatedMessage)]
      public async Task<IActionResult> Post([FromBody][ModelBinder] CategoryDtoRequest request)
      {
         if (!ModelState.IsValid)
         {
             return BadRequest(ModelState);
         }

         var response = await _service.CreateAsync(request);

         if (response.Success)
             return Created($"Category/{response.Result}", response.Result);

         return BadRequest();
      }

      [HttpPut("{id:int}")]
      [SwaggerResponse(Constants.Accepted, Constants.AcceptedMessage, typeof(int))]
      [SwaggerResponse(Constants.NotFound, Constants.NotFoundMessage, typeof(ResponseDto<int>))]
      public async Task<IActionResult> Update(int id, [FromBody] CategoryDtoRequest request)
      {
         var response = await _service.UpdateAsync(id, request);
         if (response.Success)
             return Accepted($"Category/{response.Result}");

         return NotFound(id);
      }


      [HttpDelete("{id:int}")]
      [SwaggerResponse(Constants.Accepted, Constants.AcceptedMessage, typeof(int))]
      [SwaggerResponse(Constants.NotFound, Constants.NotFoundMessage, typeof(ResponseDto<int>))]
      public async Task<IActionResult> Delete(int id)
      {
         var response = await _service.DeleteAsync(id);

         if (response.Success)
             return Accepted();

         return NotFound(id);
      }

   }
}
