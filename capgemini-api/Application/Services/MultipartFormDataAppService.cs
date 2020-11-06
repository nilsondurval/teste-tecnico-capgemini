using capgemini_api.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace capgemini_api.Application.Services
{
  public class MultipartFormDataAppService : IMultipartFormDataAppService
  {
    public async Task<MultipartContent<T>> ReadMultipartFormDataAsync<T>(HttpRequest request, string jsonObjectName)
    {
      var formData = await request.ReadFormAsync();
      var files = new List<MultipartContentFile>();

      foreach (var file in formData.Files)
      {
        files.Add(new MultipartContentFile()
        {
          Name = file.Name,
          Content = GetFileBytesFromFile(file),
        });
      }

      return new MultipartContent<T>()
      {
        JsonObject = JsonConvert.DeserializeObject<T>(formData[jsonObjectName][0]),
        Files = files,
      };
    }

    public async Task WriteMultipartFormData<T>(ControllerContext context, MultipartContent<T> content)
    {
      var multipartContent = new MultipartContent("form-data", CreateMultipartBoundary());

      AddJsonToMultipartContent(content.JsonObject, multipartContent);
      AddFilesToMultipartContent(content.Files, multipartContent);

      await multipartContent.CopyToAsync(context.HttpContext.Response.Body);
    }

    private byte[] GetFileBytesFromFile(IFormFile file)
    {
      using (var fileStream = file.OpenReadStream())
      {
        using (var memoryStream = new MemoryStream())
        {
          fileStream.CopyTo(memoryStream);
          return memoryStream.ToArray();
        }
      }
    }

    private string CreateMultipartBoundary()
    {
      return string.Format("--------------{0}{1}", "CapgeminiAPI", Guid.NewGuid().ToString());
    }

    private void AddJsonToMultipartContent(object jsonObject, MultipartContent multipartContent)
    {
      var jsonString = JsonConvert.SerializeObject(jsonObject);
      var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

      multipartContent.Add(stringContent);
    }

    private void AddFilesToMultipartContent(List<MultipartContentFile> files, MultipartContent multipartContent)
    {
      foreach (var file in files)
      {
        var byteArrayContent = new ByteArrayContent(file.Content);
        byteArrayContent.Headers.Add("Content-Type", "application/octet-stream");

        multipartContent.Add(byteArrayContent);
      }
    }
  }
}
