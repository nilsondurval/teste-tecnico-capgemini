using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using capgemini_api.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace capgemini_api.Services
{
  public class MultipartFormDataService : IMultipartFormDataService
    {
        public async Task<List<MultipartContentFile>> ReadMultipartFormDataAsync(HttpRequest request)
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

            return files;
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
    }
}