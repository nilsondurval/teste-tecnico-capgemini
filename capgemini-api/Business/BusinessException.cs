using System;
using System.Collections.Generic;
using capgemini_api.Models.Classes;

namespace capgemini_api.Business
{
  public class BusinessException : Exception
  {
    public BusinessException()
    {
    }

    public List<ErrorMessage> errors { get; set; }

    public BusinessException(List<ErrorMessage> errors)
    {
        this.errors = errors;
    }
  }
}