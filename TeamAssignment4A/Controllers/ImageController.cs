using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Newtonsoft.Json;

namespace TeamAssignment4A.Controllers {

    public class ImageController : Controller {
        //private readonly IHostingEnvironment _hostingEnvironment;
        //public ImageController(IHostingEnvironment hostingEnvironment) {
        //    _hostingEnvironment = hostingEnvironment;
        //}

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile upload) {
            if (upload == null || upload.Length == 0) {
                return BadRequest("File not provided");
            }
            string guidFileName = Guid.NewGuid() + Path.GetExtension(upload.FileName);
            string currentDir = Environment.CurrentDirectory;
            var filePath = Path.Combine(currentDir, "Images", guidFileName);
            //var filePath = Path.Combine(currentDir, "Images", upload.FileName);  // new Guid
            using (var stream = new FileStream(filePath, FileMode.Create)) {
                await upload.CopyToAsync(stream);
            }
            var data = new {
                uploaded = 1,
                fileName = guidFileName,
                url = $"/Images/{guidFileName}"
            };
            var json = JsonConvert.SerializeObject(data);
            return Content(json, "application/json");
        }
        //[HttpPost]
        //public async Task<string> UploadImage() {
        //    dynamic payload = Newtonsoft.Json.JsonConvert.DeserializeObject(await Request.());
        //    var fileBytes = Convert.FromBase64String((string)payload.upload);
        //    // Do something with the binary file, such as saving it to a database or cloud storage
        //    return "{}";
        //}


    }
}
