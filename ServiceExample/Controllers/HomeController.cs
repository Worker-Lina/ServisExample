using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceExample.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ServiceExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _appEnvironment;
        public HomeController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }


        public IActionResult GetFile(int id)
        {
            using (FileContext db = new FileContext())
            {
                var fil  = (from file in db.Files
                             where file.Id == id
                             select file).SingleOrDefault();
                string path = @"Files/" + fil.Name;
                string file_path = Path.Combine(_appEnvironment.ContentRootPath, path);
                return PhysicalFile(file_path, System.Net.Mime.MediaTypeNames.Application.Octet, fil.Name);
            }
        }



        public void DownloadDocument(string link)
        {
            //string link = @"https://animalworld.com.ua/images/2009/October_09/Foto/Sean_crane/Animal_sean_crane_61.jpg";
            using (var client = new WebClient())
            {
                String file_name = Path.GetFileName(link);

                client.DownloadFile(link, @"Files/"+ file_name);

                HttpWebRequest r0 = (HttpWebRequest)HttpWebRequest.Create(link);
                r0.Method = "GET";
                HttpWebResponse res = (HttpWebResponse)r0.GetResponse();
                string size = (int.Parse(res.ContentLength.ToString()) / 1024).ToString();


                var index = link.LastIndexOf(@".");
                index++;
                string type = link.Substring(index, link.Length - index);

                using (FileContext db = new FileContext())
                {
                    db.Files.Add( new Models.File
                    {
                        Name = file_name,
                        Size = size,
                        DateAdd = DateTime.Now,
                        MineType = type
                    });
                    db.SaveChanges();
                }
            }
        }

        public JsonResult GetFileJson(int id)
        {
            using (FileContext db = new FileContext())
            {
                var fil = (from file in db.Files
                           where file.Id == id
                           select file).SingleOrDefault();

                return Json(fil);
            }
        }

        public FileResult GetBytes(int id, string file_name, string type)
        {
            using (FileContext db = new FileContext())
            {
                var fil = (from file in db.Files
                           where file.Id == id
                           select file).SingleOrDefault();
                string path = Path.Combine(_appEnvironment.ContentRootPath, @"Files/" + fil.Name);
                byte[] mas = System.IO.File.ReadAllBytes(path);
                string file_type = "application/" + type;
                file_name += "." + type;
                return File(mas, file_type, file_name);
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public int Hello()
        {
            return 4+7;
        }
    }
}
