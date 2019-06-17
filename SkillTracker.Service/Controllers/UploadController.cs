using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Linq;
using SkillTracker.Data.Models;

namespace SkillTracker.Service.Controllers
{
    [ExcludeFromCoverage]
    public class UploadController : ApiController
    {
        public async Task<HttpResponseMessage> Post()
        {
            // Check whether the POST operation is MultiPart or not
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // Prepare CustomMultipartFormDataStreamProvider in which our multipart form
            // data will be loaded.
            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data");

            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            List<string> files = new List<string>();

            try
            {
                // Read all contents of multipart message into CustomMultipartFormDataStreamProvider.
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (MultipartFileData file in provider.FileData)
                {
                    files.Add(Path.GetFileName(file.LocalFileName));
                }

                // Send OK Response along with saved file names to the client.
                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [Route("api/Associates/FetchImage")]
        [HttpPost]
        public IHttpActionResult GetImageURL(Image file)
        {
            Image outputFile = new Image();

            try
            {
                string imgBase64String = Util.GetBase64StringForImage(file.FileName);
                outputFile.Base64StringSrc = imgBase64String;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return Ok(outputFile);
        }
    }

    // We implement MultipartFormDataStreamProvider to override the filename of File which
    // will be stored on server, or else the default name will be of the format like Body-
    // Part_{GUID}. In the following implementation we simply get the FileName from 
    // ContentDisposition Header of the Request Body.
    [ExcludeFromCoverage]
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            var originalFileName = headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            var serverFileName = Util.GetServerFileName(originalFileName);

            return serverFileName;
        }
    }

    public class Util
    {
        public static string GetServerFileName(string fileName, string randomSeed = "")
        {
            string outputName = string.Empty;

            var fileNameArray = fileName.Split('.');
            var extension = fileNameArray[fileNameArray.Length - 1];
            var name = string.Join(".", fileNameArray.Take(fileNameArray.Length - 1));

            var randomString = string.IsNullOrWhiteSpace(randomSeed) ? DateTime.Now.Ticks.ToString() : randomSeed;

            outputName = name + "_" + randomString + "." + extension;

            return outputName;
        }

        [ExcludeFromCoverage]
        public static string GetBase64StringForImage(string fileName)
        {
            string imageURL = string.Empty;

            try
            {
                var fileNameArray = fileName.Split('.');
                var extension = fileNameArray[fileNameArray.Length - 1];

                string savedFileLocation = HttpContext.Current.Server.MapPath("~/App_Data");
                savedFileLocation += "\\" + fileName;

                byte[] imageBytes = File.ReadAllBytes(savedFileLocation);
                string base64String = Convert.ToBase64String(imageBytes);
                imageURL = string.Concat(string.Format("data:image/{0};base64,", extension.ToLower()), base64String);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return imageURL;
        }
    }
}
