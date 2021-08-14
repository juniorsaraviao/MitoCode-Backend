using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MitoCodeExam.Dto.Request;
using MitoCodeExam.Dto.Response;
using MitoCodeExam.Entities;
using MitoCodeExam.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace MitoCodeExam.Controllers
{
   [EnableCors("_allowSpecificOrigins")]
   [Route(Constants.RouteTemplate)]
   [ApiVersion(Constants.V1)]
   [ApiController]
   public class ProductController : ControllerBase
   {
      private readonly IProductService _productService;
      public ProductController(IProductService productService)
      {
         _productService = productService;
      }

      [HttpGet]
      [SwaggerResponse(Constants.Ok, Constants.ReadyMessage, typeof(ProductDtoResponse))]
      public async Task<IActionResult> Get([FromQuery] string filter, int page = 1, int rows = 4)
      {
         return Ok( await _productService.GetCollectionAsync( new BaseDtoRequest( filter, page, rows ) ) );
      }

      [HttpGet]
      [Route("{id:int}")]
      [SwaggerOperation()]
      [SwaggerResponse(Constants.Ok, Description = Constants.ReadyMessage, Type = typeof(ResponseDto<ProductDtoSingleResponse>))]
      [SwaggerResponse(Constants.NotFound, Description = Constants.NotFoundMessage, Type = typeof(ResponseDto<ProductDtoSingleResponse>))]
      public async Task<IActionResult> Get(int id)
      {
         var response = await _productService.GetProductAsync(id);
         return response.Success ? Ok(response) : NotFound();
      }

      [HttpPost]
      [SwaggerResponse(Constants.Created, Constants.CreatedMessage)]
      public async Task<IActionResult> Post([FromBody][ModelBinder] ProductDtoRequest request)
      {
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }
         var response = await _productService.CreateAsync(request);
         if(response.Success)
               return Created($"Product/{response.Result}", response.Result);
        
         return BadRequest();
      }

      [HttpPut("{id:int}")]
      [SwaggerResponse(Constants.Accepted, Constants.AcceptedMessage, typeof(int))]
      [SwaggerResponse(Constants.NotFound, Constants.NotFoundMessage, typeof(ResponseDto<int>))]
      public async Task<IActionResult> Update(int id, [FromBody] ProductDtoRequest request)
      {
         var response = await _productService.UpdateAsync(id, request);
         if ( response.Success )
            return AcceptedAtAction("Get", response.Result, request);

         return NotFound(id);
      }

      [HttpDelete("{id:int}")]
      [SwaggerResponse(Constants.Accepted, Constants.AcceptedMessage, typeof(int))]
      [SwaggerResponse(Constants.NotFound, Constants.NotFoundMessage, typeof(ResponseDto<int>))]
      public async Task<IActionResult> Delete(int id)
      {
         var response = await _productService.DeleteAsync(id);
         if( response.Success )
            return Accepted();

         return NotFound(id);
      }
   }
}
