using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace capgemini_api.Services.Interfaces
{
  public interface IMultipartFormDataService
    {
        Task<List<MultipartContentFile>> ReadMultipartFormDataAsync(HttpRequest request);
    }

    public class MultipartContentFile
    {
        public byte[] Content { get; set; }
        public string Name { get; set; }
    }
}