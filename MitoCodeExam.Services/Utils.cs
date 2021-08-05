using System;

namespace MitoCodeExam.Services
{
   public class Utils
   {
      public static int GetTotalPages(int total, int rows)
      {
         return (int)Math.Ceiling(total / (double)rows);
      }
   }
}
