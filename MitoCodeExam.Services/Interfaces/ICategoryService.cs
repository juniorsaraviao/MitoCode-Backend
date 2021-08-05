using MitoCodeExam.Dto.Request;
using MitoCodeExam.Dto.Response;
using System.Threading.Tasks;

namespace MitoCodeExam.Services.Interfaces
{
   public interface ICategoryService
   {
      Task<CategoryDtoResponse> GetCollectionAsync(BaseDtoRequest request);

      Task<ResponseDto<CategoryDtoSingleResponse>> GetAsync(int id);

      Task<ResponseDto<int>> CreateAsync(CategoryDtoRequest request);

      Task<ResponseDto<int>> UpdateAsync(int id, CategoryDtoRequest request);

      Task<ResponseDto<int>> DeleteAsync(int id);
   }
}
