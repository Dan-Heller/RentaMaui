using Microsoft.Extensions.Configuration;

namespace Renta.Services
{
    public class FileService
    {
        IConfiguration configuration;

        public FileService(IConfiguration config)
        {
            configuration = config;
        }

        public async Task<string> UploadImageAsync(Stream image, string fileName)
        {
            HttpContent fileStreamContent = new StreamContent(image);
            fileStreamContent.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                    { Name = "file", FileName = fileName };
            fileStreamContent.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent);
                var response =
                    await client.PostAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Files/upload"),
                        formData);
                string imageUrl = await response.Content.ReadAsStringAsync();

                return imageUrl;
            }
        }
    }
}