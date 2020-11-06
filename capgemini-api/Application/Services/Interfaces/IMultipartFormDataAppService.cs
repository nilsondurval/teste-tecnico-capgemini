﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace capgemini_api.Application.Services.Interfaces
{
  public interface IMultipartFormDataAppService
  {
    Task<MultipartContent<T>> ReadMultipartFormDataAsync<T>(HttpRequest request, string jsonObjectName);
    Task WriteMultipartFormData<T>(ControllerContext context, MultipartContent<T> content);
  }

  public class MultipartContent<T>
  {
    public T JsonObject { get; set; }
    public List<MultipartContentFile> Files { get; set; }
  }

  public class MultipartContentFile
  {
    public byte[] Content { get; set; }
    public string Name { get; set; }
  }
}
