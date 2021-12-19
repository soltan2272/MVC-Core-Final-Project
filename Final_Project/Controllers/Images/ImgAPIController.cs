using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Models;
using Reposotries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImgAPIController : ControllerBase
    {
        public static IWebHostEnvironment WebHostEnvironment;
        IUnitOfWork UnitOfWork;
        public ImgAPIController(IWebHostEnvironment webHostEnvironment,IUnitOfWork unitOfWork)
        {
            WebHostEnvironment = webHostEnvironment;
            UnitOfWork = unitOfWork;
        }
        [HttpPost]
        public string post([FromForm]FileUplode fileUplode)
        {
            try
            {
                if (fileUplode.files.Length>0)
                {
                    string path = WebHostEnvironment.WebRootPath + "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string datetime = System.DateTime.Now.ToString("ddMMyyyyHHMMss");
                    using(FileStream fileStream = System.IO.File.Create(path + datetime + fileUplode.files.FileName))
                    {
                        fileUplode.files.CopyTo(fileStream);
                        fileStream.Flush();
                        //Images image = new Images();
                        //image.CurrentProductID = proID;
                        //image.Image_URL = path+ datetime + fileUplode.files.FileName;
                        //Product pro = UnitOfWork.context().Products.Where(p => p.ID == image.CurrentProductID).FirstOrDefault();
                        //if (pro != null)
                        //{
                        //    image.product = pro;
                        //}
                        //UnitOfWork.context().Images.Add(image);
                        //UnitOfWork.context().SaveChanges();                      
                        return "Uploaded Image";
                    }
                }
                else
                {
                    return "Not Uploaded";
                }


            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
