using System;
using System.Collections.Generic;
using capgemini_api.Domain.Models.Classes;
using capgemini_api.Domain.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Arrecadacao_core.Controllers
{
  public class BaseController : Controller
  {
    [NonAction]
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      base.OnActionExecuting(context);
    }

    [NonAction]
    public BadRequestObjectResult BadRequest(ErrorEnum codigo, String errorMessage)
    {
      var listError = new List<ErrorMessage>();

      listError.Add(new ErrorMessage { Codigo = codigo, Message = errorMessage });

      return base.BadRequest(new { errors = listError });
    }

    [NonAction]
    public BadRequestObjectResult NotFound(ErrorEnum codigo, String errorMessage)
    {
      var listError = new List<ErrorMessage>();

      listError.Add(new ErrorMessage { Codigo = codigo, Message = errorMessage });

      return base.BadRequest(new { errors = listError });
    }
  }
}