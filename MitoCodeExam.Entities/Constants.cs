using System;
using System.Collections.Generic;
using System.Text;

namespace MitoCodeExam.Entities
{
   public class Constants
   {
      public const string RouteTemplate = "api/v1/[controller]";
      public const string V1            = "1.0";

      public const int    Ok            = 200;
      public const int    Created       = 201;
      public const int    Accepted      = 202;
      public const int    NotFound      = 404;
      public const int    BadRequest    = 400;
      public const int    Unauthorized  = 401;

      public const string AcceptedMessage     = "Aceptado";
      public const string CreatedMessage      = "Creado";
      public const string ReadyMessage        = "Ok";
      public const string NotFoundMessage     = "No Encontrado";
      public const string NotValidMessage     = "No Valido";
      public const string UnauthorizedMessage = "No Autorizado";
   }
}
