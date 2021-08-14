using Microsoft.Extensions.Logging;
using MitoCodeExam.DataAccess.Interfaces;
using MitoCodeExam.Dto.Request;
using MitoCodeExam.Dto.Response;
using MitoCodeExam.Entities;
using MitoCodeExam.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MitoCodeExam.Services.Implementations
{
   public class ProductService : IProductService
   {
      private readonly IProductRepository          _productRepository;
      private readonly ILogger<IProductRepository> _logger;

      public ProductService(IProductRepository productRepository, ILogger<IProductRepository> logger)
      {
         _productRepository = productRepository;
         _logger            = logger;
      }

      public async Task<ProductDtoResponse> GetCollectionAsync(BaseDtoRequest request)
      {
         var response = new ProductDtoResponse();

         var tupla = await _productRepository.GetCollectionAsync(request.Filter ?? string.Empty, request.Page, request.Rows);
         response.Collection = tupla.collection.Select(x => new ProductDto
         {
            ProductId   = x.Id,
            Category    = x.CategoryName,
            ProductName = x.Name,
            UnitPrice   = x.UnitPrice,
            Enabled     = x.Enabled
         }).ToList();

         response.TotalPages = Utils.GetTotalPages(tupla.total, request.Rows);
         return response;
      }

      public async Task<ResponseDto<int>> CreateAsync(ProductDtoRequest request)
      {
         var response = new ResponseDto<int>();

         try
         {            

            response.Result =  await _productRepository.CreateAsync( new Product
            {
               Name       = request.ProductName,
               CategoryId = request.CategoryId,
               UnitPrice  = request.ProductPrice,
               Enabled    = request.ProductEnabled
            });
            response.Success = true;
         }
         catch (Exception ex)
         {
            _logger.LogCritical(ex.Message);
            response.Success = false;
         }

         return response;
      }

      public async Task<ResponseDto<ProductDtoSingleResponse>> GetProductAsync(int id)
      {
         var response = new ResponseDto<ProductDtoSingleResponse>();

         try
         {
            var product  = await _productRepository.GetItemAsync(id);

            if(product == null)
            {
               response.Success = false;
               return response;
            }

            response.Result = new ProductDtoSingleResponse
            {
               ProductId       = product.Id,
               ProductName     = product.Name,
               ProductPrice    = product.UnitPrice,
               CategoryId      = product.CategoryId,
               ProductEnabled  = product.Enabled
            };

            response.Success = true;            
         }
         catch (Exception ex)
         {
            _logger.LogCritical(ex.Message);
            response.Success = false;
         }

         return response;
      }

      public async Task<ResponseDto<int>> UpdateAsync(int id, ProductDtoRequest request)
      {
         var response = new ResponseDto<int>();

         try
         {

            await _productRepository.UpdateAsync(new Product
            {
               Id         = id,
               Name       = request.ProductName,
               CategoryId = request.CategoryId,
               UnitPrice  = request.ProductPrice,
               Enabled    = request.ProductEnabled
            });

            response.Result  = id;
            response.Success = true;
         }
         catch (Exception ex)
         {
            _logger.LogCritical(ex.Message);
            response.Success = false;
         }

         return response;
      }

      public async Task<ResponseDto<int>> DeleteAsync(int id)
      {
         var response = new ResponseDto<int>();

         try
         {
            await _productRepository.DeleteAsync(id);
            response.Result  = id;
            response.Success = true;
         }
         catch (Exception ex)
         {
            _logger.LogCritical(ex.Message);
            response.Success = false;
         }

         return response;
      }
   }
}
