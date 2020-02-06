using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace UpLoadFile.Controllers
{
    public class FileController : Controller
    {
        #region Initial
        private readonly IHostingEnvironment _env;
        private readonly string rootPath = "";
        public FileController(IHostingEnvironment env)
        {
            _env = env;
            rootPath = _env.WebRootPath + "\\Uploads";
        }
        #endregion

        #region Single File UpLoad
        public IActionResult SingleFile(IFormFile file)
        {
            var rootPath = _env.WebRootPath+"\\Uploads";
            using(var fileStream = new FileStream(Path.Combine(rootPath, file.FileName),FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }
            return RedirectToAction("Index","Home", new { file=file.FileName });
        }
        #endregion

        #region Multiple File Uploads
        public IActionResult MultipleFile(IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                using (var fileStream = new FileStream(Path.Combine(rootPath, file.FileName), FileMode.Create, FileAccess.Write))
                {
                    file.CopyToAsync(fileStream);
                }
            }
            return RedirectToAction("Index","Home");
        }
        #endregion
    }
}