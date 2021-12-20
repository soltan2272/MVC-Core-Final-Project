using AdminDashboard.Models;
using Final_Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models;
using PagedList;
using PagedList.Mvc;
using ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Models.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminDashboard.Controllers
{

    public class AdminDashboardController : Controller
    {
        private readonly ILogger<AdminDashboardController> _logger;
       
        public AdminDashboardController(ILogger<AdminDashboardController> logger)
        {
            _logger = logger;
          

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Logincheck(LoginModel logininfo)
        {
            
            bool isAdmin = false;
            var jsondata = JsonConvert.SerializeObject(logininfo);
            var data = new StringContent(jsondata, Encoding.UTF8, "application/json");
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(Global.API);
            var AdminLogin = http.PostAsync("Admin/login", data);
            AdminLogin.Wait();
            var resltadmin = AdminLogin.Result;
            string result = null;
            if (resltadmin.IsSuccessStatusCode)
            {
                var tabel = resltadmin.Content.ReadAsAsync<AuthModel>();
                tabel.Wait();
                var ser = tabel.Result.Token;
                var usrid = tabel.Result.User_ID;
                var usrname = tabel.Result.Username;
                var Roles = tabel.Result.Roles;
                foreach(string role in Roles)
                {
                    if(role=="Admin")
                    {
                        isAdmin = true;
                    }
                }
               if(isAdmin==false)
                {
                    return Redirect("/AdminDashboard/Login");
                }
                result = ser;
                HttpContext.Response.Cookies.Append("UserToken", ser);
                HttpContext.Response.Cookies.Append("UserTokenCheck", ser);
                HttpContext.Response.Cookies.Append("UserID", usrid.ToString());
                HttpContext.Response.Cookies.Append("UserName", usrname);
                ViewData["UserName"] = tabel.Result.Username;
                return Redirect("/AdminDashboard/Index");

            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }

        }

        public IActionResult Users(int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<User> users = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var userscontroller = http.GetAsync("User/getusers");
                userscontroller.Wait();
                var resltuser = userscontroller.Result;
                if (resltuser.IsSuccessStatusCode)
                {
                    var tabel = resltuser.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);



                    users = JsonConvert.DeserializeObject<IList<User>>(jsonString);
                }
                return View(users.ToPagedList((p ?? 1), 6));
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }



        }

        public IActionResult Suppliers(int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<User> Sellers = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var sellerscontroller = http.GetAsync("Seller/getsellers");
                sellerscontroller.Wait();
                var resltuser = sellerscontroller.Result;
                if (resltuser.IsSuccessStatusCode)
                {
                    var tabel = resltuser.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);
                    Sellers = JsonConvert.DeserializeObject<IList<User>>(jsonString);
                }

                return View(Sellers.ToPagedList((p ?? 1), 6));
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }

        }
        public IActionResult Admins(int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<User> Admins = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var adminscontroller = http.GetAsync("Admin/getadmins");
                adminscontroller.Wait();
                var resltuser = adminscontroller.Result;
                if (resltuser.IsSuccessStatusCode)
                {
                    var tabel = resltuser.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);



                    Admins = JsonConvert.DeserializeObject<IList<User>>(jsonString);
                }
                return View(Admins.ToPagedList((p ?? 1), 6));
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }

        }




        public IActionResult DeleteUser(int id)
       {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("User/deleteuser/" + id);
                response.Wait();
                return Redirect("/AdminDashboard/Users");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        public IActionResult DeleteSeller(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("Seller/deleteseller/" + id);
                response.Wait();
                return Redirect("/AdminDashboard/Suppliers");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }



        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies["UserToken"] == HttpContext.Request.Cookies["UserTokenCheck"])
            {
                
                Statistics statistics = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);

                var statisticscontroller = http.GetAsync("Report/Statistics");
                statisticscontroller.Wait();
                var resltorder = statisticscontroller.Result;
                if (resltorder.IsSuccessStatusCode)
                {
                    var tabel = resltorder.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var serialiaze = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(serialiaze);
                    statistics = JsonConvert.DeserializeObject<Statistics>(jsonString);
                }


                return View(statistics);
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        public IActionResult Orders(int? p = 1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<Order> orders = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);

                var ordercontroller = http.GetAsync("User");
                ordercontroller.Wait();
                var resltorder = ordercontroller.Result;
                if (resltorder.IsSuccessStatusCode)
                {
                    var tabel = resltorder.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var serialiaze = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(serialiaze);
                    orders = JsonConvert.DeserializeObject<IList<Order>>(jsonString);
                }

                return View(orders.ToPagedList((p ?? 1), 6));

            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> editOrder(Order order)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = TimeSpan.FromSeconds(60);
                client.BaseAddress = new Uri("https://localhost:44354/");
                var data = new
                {
                    Quantity = order.Quantity,
                    Order_Date = order.Order_Date,
                    CurrentUserID = order.CurrentUserID,
                    Delivery_Status = order.Delivery_Status,
                    ID = order.ID
                };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PutAsync("api/User/EditOrder/" + order.ID, content))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    response.EnsureSuccessStatusCode();
                    return Redirect("/AdminDashboard/Orders");

                }
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

      

        public  IActionResult deleteOrders(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("User/deleteorder/"+ id);
                response.Wait();

                return Redirect("/AdminDashboard/Orders");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        [HttpGet]
        public IActionResult Products(int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<Product> products = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var productcontroller = http.GetAsync("product/AdminProducts/");
                //var productcontroller = http.GetAsync("Product/AdminProducts");
                productcontroller.Wait();
                var resltproduct = productcontroller.Result;
                if (resltproduct.IsSuccessStatusCode)
                {
                    var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);
               
                    products = JsonConvert.DeserializeObject<IList<Product>>(jsonString);
                }

                return View(products.ToPagedList((p ?? 1), 6));
            }

            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("product/Delete/" + id);
                response.Wait();

                return Redirect("/AdminDashboard/Products/1");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        [HttpGet]
        public IActionResult Detiles(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                Product product = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var productcontroller = http.GetAsync("product/AdminProduct/" + id);
                productcontroller.Wait();
                var resltproduct = productcontroller.Result;
                if (resltproduct.IsSuccessStatusCode)
                {
                    var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    if (tabel.Result.ISuccessed == false)
                        return View("NotFound");

                    var data = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(data);

                    product = JsonConvert.DeserializeObject<Product>(jsonString);
                }

                return View(product);
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

  
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> AddProduct(InsertProductViewModel product)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                var myAccount = new Account { ApiKey = "325458487894941", ApiSecret = "HF8vRpQ2VL-LuPb6lHdXKYAaZNg", Cloud = "dppeduocd" };
                Cloudinary _cloudinary = new Cloudinary(myAccount);
                for (var i = 0; i < product.imgspathes.Length; i++)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        //File = new FileDescription(@"wwwroot\img\zzz.png")
                        File = new FileDescription(@"wwwroot\img\uploads\" + product.imgspathes[i])
                    };
                    var uploadResult = _cloudinary.Upload(uploadParams);

                    string img = uploadResult.SecureUri.AbsoluteUri;
                    product.imgspathes[i] = img;
                }
                //var myAccount = new Account { ApiKey = "325458487894941", ApiSecret = "HF8vRpQ2VL-LuPb6lHdXKYAaZNg", Cloud = "dppeduocd" };
                //Cloudinary _cloudinary = new Cloudinary(myAccount);

                //var uploadParams = new ImageUploadParams()
                //{
                //    //File = new FileDescription(@"wwwroot\img\zzz.png")
                //    File = new FileDescription(@"wwwroot\img\uploads\" + product.imgspathes[0])
                //};
                //var uploadResult = _cloudinary.Upload(uploadParams);

                //string img = uploadResult.SecureUri.AbsoluteUri;

                var sallerid = HttpContext.Request.Cookies["UserID"];
                var sid = int.Parse(sallerid);
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = TimeSpan.FromSeconds(60);
                client.BaseAddress = new Uri("https://localhost:44354/");
                var data = new
                {
                    ID = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Rate = product.Rate,
                    imgspathes = product.imgspathes,
                    Description = product.Description,
                    CurrentSupplierID = sid,
                    CurrentCategoryID = Request.Form["catID"].ToString() //product. .CurrentCategoryID
                };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync("api/Product/addProduct", content))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();

                    return Redirect("/AdminDashboard/productMyStore");
                }
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        public IActionResult AddProduct()
        {
            IEnumerable<Category> cattt = null;
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(Global.API);
            var catList = http.GetAsync("product/category");
            catList.Wait();
            var resltcat = catList.Result;
            if (resltcat.IsSuccessStatusCode)
            {
                var tabel2 = resltcat.Content.ReadAsAsync<ResultViewModel>();
                tabel2.Wait();
                var cat = tabel2.Result.Data;
                string jsonString = JsonConvert.SerializeObject(cat);

                cattt = JsonConvert.DeserializeObject<IList<Category>>(jsonString);
                ViewBag.categories = new SelectList(cattt, "ID", "Name");
            }
            return View();
        }

        [HttpPost]
        //public async Task<IActionResult> editProduct(int id, Product product)
        //{
        public async Task<IActionResult> editProduct(Product product)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = TimeSpan.FromSeconds(60);
                client.BaseAddress = new Uri("https://localhost:44354/");
                var data = new
                {
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    //Image = product.Image,
                    Rate = product.Rate,
                    Description = product.Description,
                    CurrentSupplierID = product.CurrentSupplierID,
                    CurrentCategoryID = product.CurrentCategoryID,
                    ID = product.ID
                };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PutAsync("api/Product/editproduct/"+product.ID, content))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();
                    return Redirect("/AdminDashboard/Products");

                }
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }

        }

        public IActionResult editProduct()
        {
            return View();
        }

        public IActionResult AddAdmin()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(SignUpModel admin)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                if (ModelState.IsValid)
                {

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = TimeSpan.FromSeconds(60);
                    client.BaseAddress = new Uri("https://localhost:44354/");
                    var content = new StringContent(JsonConvert.SerializeObject(admin), Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await client.PostAsync("api/Admin/addadmin", content))
                    {
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        if (response.IsSuccessStatusCode)
                        {
                            response.EnsureSuccessStatusCode();
                            return Redirect("/AdminDashboard/Admins");
                        }
                        else
                        {
                            return Redirect("/AdminDashboard/AddAdmin");
                        }
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        public IActionResult changepassword()
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                return View();
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        [HttpPost]
        public async Task<IActionResult> changepassword(ChangePassword admin)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                if (ModelState.IsValid)
                {

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = TimeSpan.FromSeconds(60);
                    client.BaseAddress = new Uri("https://localhost:44354/");
                    var content = new StringContent(JsonConvert.SerializeObject(admin), Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await client.PostAsync("api/User/changepassword", content))
                    {
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        if (response.IsSuccessStatusCode)
                        {
                            response.EnsureSuccessStatusCode();
                            return Redirect("/AdminDashboard/Admins");
                        }
                        else
                        {
                            return Redirect("/AdminDashboard/changepassword");
                        }
                    }
                }
                else
                {
                    return View();
                }

            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        public IActionResult logout()
        {
            HttpContext.Response.Cookies.Append("UserToken", "");
            HttpContext.Response.Cookies.Append("UserID", "");
            HttpContext.Response.Cookies.Append("UserName", "");
            return Redirect("/Login");
        }

        public IActionResult Stores(int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<User> Sellers = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var sellerscontroller = http.GetAsync("Seller/getsellers");
                sellerscontroller.Wait();
                var resltuser = sellerscontroller.Result;
                if (resltuser.IsSuccessStatusCode)
                {
                    var tabel = resltuser.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);
                    Sellers = JsonConvert.DeserializeObject<IList<User>>(jsonString);
                }

                return View(Sellers.ToPagedList((p ?? 1), 6));
            }
            else
            {
                return Redirect("/AdminDashboard/Login");

               
            }
        }
        [HttpGet]
        public IActionResult StoreProduct(int id,int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<Product> products = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var productcontroller = http.GetAsync("search/Seller/" + id);
                //var productcontroller = http.GetAsync("Product/AdminProducts");
                productcontroller.Wait();
                var resltproduct = productcontroller.Result;
                if (resltproduct.IsSuccessStatusCode)
                {
                    var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);


                    products = JsonConvert.DeserializeObject<IList<Product>>(jsonString);
                }
                return View(products.ToPagedList((p ?? 1), 6));
            }

            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }


       

        [HttpGet]
        public IActionResult StoreDetiles(int id )
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                Product product = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var productcontroller = http.GetAsync("product/AdminProduct/" + id);
                productcontroller.Wait();
                var resltproduct = productcontroller.Result;
                if (resltproduct.IsSuccessStatusCode)
                {



                    var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    if (tabel.Result.ISuccessed == false)
                        return View("NotFound");

                    var data = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(data);

                    product = JsonConvert.DeserializeObject<Product>(jsonString);
                }

                return View(product);
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }



        [HttpGet]
        public IActionResult DeleteProStore(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("product/Delete/" + id);
                response.Wait();

                return Redirect("/AdminDashboard/Stores/1");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        public IActionResult MyStore(int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                var sallerid = HttpContext.Request.Cookies["UserID"];
                var sid = int.Parse(sallerid);
                IEnumerable<Product> products = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var productcontroller = http.GetAsync("Search/Seller/"+sid);
                productcontroller.Wait();
                var resltproduct = productcontroller.Result;
                if (resltproduct.IsSuccessStatusCode)
                {
                    var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);


                    products = JsonConvert.DeserializeObject<IList<Product>>(jsonString);
                }
                return View(products.ToPagedList((p ?? 1), 6));
            }

            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        public IActionResult ContactUs(int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<ContactUs> ContactUs = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var Usercontroller = http.GetAsync("User/getContactUs");
                Usercontroller.Wait();
                var resltproduct = Usercontroller.Result;
                if (resltproduct.IsSuccessStatusCode)
                {
                    var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);


                    ContactUs = JsonConvert.DeserializeObject<IList<ContactUs>>(jsonString);
                }
                    return View(ContactUs.ToPagedList((p ?? 1), 6));
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        public IActionResult DisplayContact(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                return View();
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        public IActionResult NotFound()
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                return View();
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        public IActionResult Categories(int? p=1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<Category> categories = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var sellerscontroller = http.GetAsync("product/category");
                sellerscontroller.Wait();
                var resltuser = sellerscontroller.Result;
                if (resltuser.IsSuccessStatusCode)
                {
                    var tabel = resltuser.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);
                    categories = JsonConvert.DeserializeObject<IList<Category>>(jsonString);
                }

                return View(categories.ToPagedList((p ?? 1), 6));
            }
            else
            {
                return Redirect("/AdminDashboard/Login");


            }
        }

        public IActionResult DeleteCategory(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("product/Deletecategory/" + id);
                response.Wait();

                return Redirect("/AdminDashboard/Categories");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        public IActionResult CategoryProducts(int id, int? p = 1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<Product> products = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var productcontroller = http.GetAsync("search/Category/" + id);
                productcontroller.Wait();
                var resltproduct = productcontroller.Result;
                if (resltproduct.IsSuccessStatusCode)
                {
                    var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var ser = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(ser);


                    products = JsonConvert.DeserializeObject<IList<Product>>(jsonString);
                }
                return View(products.ToPagedList((p ?? 1), 6));
            }

            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        public IActionResult DeleteProductCategory(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("product/Delete/" + id);
                response.Wait();

                return Redirect("/AdminDashboard/Categories");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        public IActionResult DetilesProductCategory(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                Product product = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var productcontroller = http.GetAsync("product/AdminProduct/" + id);
                productcontroller.Wait();
                var resltproduct = productcontroller.Result;
                if (resltproduct.IsSuccessStatusCode)
                {
                    var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    if (tabel.Result.ISuccessed == false)
                        return View("NotFound");

                    var data = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(data);

                    product = JsonConvert.DeserializeObject<Product>(jsonString);
                }

                return View(product);
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                if (ModelState.IsValid)
                {

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = TimeSpan.FromSeconds(60);
                    client.BaseAddress = new Uri("https://localhost:44354/");
                    var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await client.PostAsync("api/Product/addcategory", content))
                    {
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        response.EnsureSuccessStatusCode();
                        return Redirect("/AdminDashboard/Categories");

                    }
                   

                }
                else
                {
                    return View();
                }



            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

       
        [HttpGet]
        public IActionResult Updateategory(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                Category category = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var productcontroller = http.GetAsync("product/CategoryID/" + id);
                productcontroller.Wait();
                var resltproduct = productcontroller.Result;
                if (resltproduct.IsSuccessStatusCode)
                {
                    var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    if (tabel.Result.ISuccessed == false)
                        return View("NotFound");

                    var data = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(data);

                    category = JsonConvert.DeserializeObject<Category>(jsonString);
                }

                return View(category);
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Updateategory(Category category)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                if (ModelState.IsValid)
                {

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = TimeSpan.FromSeconds(60);
                    client.BaseAddress = new Uri("https://localhost:44354/");
                    var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await client.PutAsync("api/Product/Updatecategory", content))
                    {
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        response.EnsureSuccessStatusCode();
                        return Redirect("/AdminDashboard/Categories/1");

                    }


                }
                else
                {
                    return View();
                }



            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        public IActionResult Shipping(int? p = 1)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                IEnumerable<Order> orders = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);

                var ordercontroller = http.GetAsync("User");
                ordercontroller.Wait();
                var resltorder = ordercontroller.Result;
                if (resltorder.IsSuccessStatusCode)
                {
                    var tabel = resltorder.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    var serialiaze = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(serialiaze);
                    orders = JsonConvert.DeserializeObject<IList<Order>>(jsonString);
                }

                return View(orders.ToPagedList((p ?? 1), 6));

            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }

        [HttpGet]
        public IActionResult LocationStatus(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                Order order = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var productcontroller = http.GetAsync("User/OrderID/" + id);
                productcontroller.Wait();
                var resltproduct = productcontroller.Result;
                if (resltproduct.IsSuccessStatusCode)
                {
                    var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    if (tabel.Result.ISuccessed == false)
                        return View("NotFound");

                    var data = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(data);

                    order = JsonConvert.DeserializeObject<Order>(jsonString);
                }

                return View(order);
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }

        }
        [HttpPost]
        public async Task<IActionResult> LocationStatus(Order order)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                if (ModelState.IsValid)
                {

                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = TimeSpan.FromSeconds(60);
                    client.BaseAddress = new Uri("https://localhost:44354/");
                    var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await client.PatchAsync("api/User/Updateorder", content))
                    {
                        var responseContent = response.Content.ReadAsStringAsync().Result;
                        response.EnsureSuccessStatusCode();
                        return Redirect("/AdminDashboard/Shipping/1");

                    }


                }
                else
                {
                    return View();
                }



            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> LocationStatus(Order order)
        //{
        //    if (HttpContext.Request.Cookies["UserToken"] != "")
        //    {
        //        if (ModelState.IsValid)
        //        {

        //            var client = new HttpClient();
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.Timeout = TimeSpan.FromSeconds(60);
        //            client.BaseAddress = new Uri("https://localhost:44354/");
        //            var content = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");

        //            using (HttpResponseMessage response = await client.PutAsync("api/User/Updateorder/", content))
        //            {
        //                var responseContent = response.Content.ReadAsStringAsync().Result;
        //                response.EnsureSuccessStatusCode();

        //                try
        //                {
        //                    // Handle success
        //                }
        //                catch (HttpRequestException)
        //                {
                            
        //                }
        //                return Redirect("/AdminDashboard/Shipping");

        //            }


        //        }
        //        else
        //        {
        //            return View();
        //        }




        //    }
        //    else
        //    {
        //        return Redirect("/AdminDashboard/Login");
        //    }
        //}
        [HttpGet]
        public IActionResult MyStoreDetiles(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                Product product = null;
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var productcontroller = http.GetAsync("product/AdminProduct/" + id);
                productcontroller.Wait();
                var resltproduct = productcontroller.Result;
                if (resltproduct.IsSuccessStatusCode)
                {



                    var tabel = resltproduct.Content.ReadAsAsync<ResultViewModel>();
                    tabel.Wait();
                    if (tabel.Result.ISuccessed == false)
                        return View("NotFound");

                    var data = tabel.Result.Data;
                    string jsonString = JsonConvert.SerializeObject(data);

                    product = JsonConvert.DeserializeObject<Product>(jsonString);
                }

                return View(product);
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
        [HttpGet]
        public IActionResult MyStoreDelete(int id)
        {
            if (HttpContext.Request.Cookies["UserToken"] != "")
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(Global.API);
                var response = http.DeleteAsync("product/Delete/" + id);
                response.Wait();

                return Redirect("/AdminDashboard/Mystore/1");
            }
            else
            {
                return Redirect("/AdminDashboard/Login");
            }
        }
       
    }
}