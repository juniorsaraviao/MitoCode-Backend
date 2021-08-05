namespace MitoCodeExam.Dto.Response
{
   public class ResponseDto<T>
   {
      public bool Success { get; set; }
      public T    Result  { get; set; }
   }
}
