namespace MitoCodeExam.Entities
{
   public class ProductInfo
   {
      public int     Id           { get; set; }
      public string  Name         { get; set; }
      public string  CategoryName { get; set; }
      public decimal UnitPrice    { get; set; }
      public bool    Enabled      { get; set; }
   }
}
