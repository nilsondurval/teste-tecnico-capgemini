using System;
using System.Collections.Generic;
using capgemini_api.Domain.Models.Classes;

namespace capgemini_api.Domain.Exceptions
{
  public class DomainException : Exception
  {
    public DomainException()
    {
    }

    public List<ErrorMessage> errors { get; set; }

    public DomainException(List<ErrorMessage> errors)
    {
        this.errors = errors;
    }
  }
}