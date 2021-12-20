using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Reposotries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ViewModels;
using Models.Models;
using PagedList.Core;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Controllers
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {
        IGenericRepostory<Product> ProductRepo;
        IGenericRepostory<Category> CategoryRepo;
        IGenericRepostory<Store> StoreRepo;
        IGenericRepostory<Images> ImageRepo;
        IUnitOfWork UnitOfWork;
        IUserRepository UserRepository;

        Project_Context Context;
        ResultViewModel result = new ResultViewModel();

        public ProductController(Project_Context context, IUserRepository userRepository,
                                 IUnitOfWork unitOfWork)
        {
            Context = context;
            UnitOfWork = unitOfWork;
            ProductRepo = UnitOfWork.GetProductRepo();
            StoreRepo = UnitOfWork.GetStoreRepo();
            CategoryRepo = UnitOfWork.GetCategoryRepo();
            ImageRepo = UnitOfWork.GetImagesRepo();
            UserRepository = userRepository;
        }

        [HttpGet("userProducts")]
        public ResultViewModel GetforUser()
        {

            result.Message = "All Products";
            result.Data = UnitOfWork.context().Products.Select(p =>new { 
            p.ID,
            p.Name,
            p.Price,
            p.ProductOffers,
            p.category,
            p.Description,
            p.productFeedbacks,
            p.Rate,
            p.supplier,
            p.Product_Images});

            return result;
        }
        [HttpGet("AdminProducts")]
        public ResultViewModel GetforAdmin()
        {
            result.Message = "All Products";
            result.Data = UnitOfWork.context().Products.Select(p => new
            {
                p.ID,
                p.Name,
                p.Price,
                p.ProductOffers,
                p.category,
                p.Description,
                p.productFeedbacks,
                p.Rate,
                p.supplier,
                p.Product_Images,
                p.Quantity,
                p.productOrders,
            });
                //.ToPagedList((p ?? 1), 5);
            return result;
        }


        [HttpGet("UserProduct/{id}")]
        public ResultViewModel GetProductByID(int id)
        {
            var product = UnitOfWork.context().Products.Where(pro => pro.ID == id).Select(p => new {
                p.ID,
                p.Name,
                p.Price,
                p.ProductOffers,
                p.category,
                p.Description,
                p.productFeedbacks,
                p.Rate,
                p.supplier,
                p.productOrders,
                p.Product_Images

            }).FirstOrDefault();
            if (product == null)
            {
                result.ISuccessed = false;
                return result;

            }

            result.Message = " Product By ID";
            result.Data = product;



            return result;

        }

        [HttpGet("AdminProduct/{id}")]
        public ResultViewModel GetProductByIDForAdmin(int id)
        {
            var product = UnitOfWork.context().Products.Where(pro=>pro.ID==id).Select(p => new {
                p.ID,
                p.Name,
                p.Price,
                p.ProductOffers,
                p.category,
                p.Description,
                p.productFeedbacks,
                p.Rate,
                p.supplier,
                p.Product_Images,
                p.Quantity,
                p.productOrders,
            }).FirstOrDefault();
            if (product == null)
            {
                result.ISuccessed = false;
                return result;

            }
            result.Message = " Product By ID";
            result.Data = product;
            return result;

        }

        [HttpGet("GetProductBySupplierID/{id}")]
        public ResultViewModel GetProductBySupplierID(int id)
        {
            Product products = ProductRepo.Get().Where(s => s.CurrentSupplierID == id).FirstOrDefault();
            if (products == null)
            {
                result.ISuccessed = false;
                return result;
            }
            result.Message = " Product By Supplier ID";
            result.Data = products;
            return result;
        }


        [HttpPost("addProduct")]
        public async Task < ResultViewModel> addProduct(InsertProductViewModel product)
        {

            var res = product;
            result.Message = "Add Product";

            var x = ToProductExtensions.ToProductModel(product);
            
            Category Cat = CategoryRepo.Get().Where(c => c.ID == product.CurrentCategoryID).FirstOrDefault();
            if (Cat != null)
            {
                x.category = Cat;
            }
            User saller = Context.Users.Where(s => s.Id == product.CurrentSupplierID).FirstOrDefault();
            if (saller != null)
            {
                x.supplier = saller;
            }

           
            ProductRepo.Add(x);
            await UnitOfWork.Save();

            Product p =  ProductRepo.Get().OrderByDescending(i => i.ID).Take(1).FirstOrDefault();
            int z = p.ID;

            addimages(z,product);

            result.Data = product;
            return result;
        }

        public ResultViewModel addimages(int id, InsertProductViewModel imgPathes)
        {
           

            foreach (var i in imgPathes.imgspathes)
            {
               
                ImageRepo.Add(new Images { CurrentProductID = id, Image_URL = i });
            }

            UnitOfWork.Save();
            result.Data = imgPathes;
            return result;
        }
        [HttpPut("UpdateProduct")]
        public async Task<ResultViewModel> UpdateProduct(InsertProductViewModel product)
        {

            var res = product;
            result.Message = "Add Product";

            var x = ToProductExtensions.ToProductModel(product);

            Category Cat = CategoryRepo.Get().Where(c => c.ID == product.CurrentCategoryID).FirstOrDefault();
            if (Cat != null)
            {
                x.category = Cat;
            }
            
            ProductRepo.Update(x);
            await UnitOfWork.Save();
            if (product.imgspathes != null)
            {
                Product p = ProductRepo.Get().OrderByDescending(i => i.ID).Take(1).FirstOrDefault();
                int z = p.ID;

                addimages(z, product);

                result.Data = product;
            }
           
            return result;
        }

        //[HttpPost("addProduct")]
        //public ResultViewModel addProduct(Product product)
        //{
        //    //StoreProduct sp = new StoreProduct();
        //    var res = product;
        //    result.Message = "Add Product";


        //    Category Cat = CategoryRepo.Get().Where(c => c.ID == product.CurrentCategoryID).FirstOrDefault();
        //    if (Cat != null)
        //    {
        //        product.category = Cat;
        //    }
        //    User saller =Context.Users.Where(s=>s.Id==product.CurrentSupplierID).FirstOrDefault();
        //        if (saller != null)
        //    {
        //        product.supplier = saller;
        //    }

        //    ProductRepo.Add(product);
        //    UnitOfWork.Save();
        //    result.Data = product;

        //    return result;
        //}
        //[HttpPost("addimages")]
        //public ResultViewModel addimages(Images image)
        //{
        //    var res = image;
        //    result.Message = "Add Images for Product";
        //    Product pro = ProductRepo.Get().Where(p => p.ID == image.CurrentProductID).FirstOrDefault();
        //    if (pro != null)
        //    {
        //        image.product = pro;
        //    }
        //    ImageRepo.Add(image);
        //    UnitOfWork.Save();
        //    result.Data = image;

        //    return result;
        //}

        [HttpPut("editProduct/{id}")]
        public ResultViewModel editProduct(int id, InsertProductViewModel pro)
        {
            //if (id == null)
            //{
            //    result.Message = "Not Found Product";
            //}
            var product = ProductRepo.GetByID(id);
            product.ID = id;
            product.Name = pro.Name;
            product.Quantity = pro.Quantity;
         

           // product.Image = pro.Image;
            product.Rate = pro.Rate;
            product.Description = pro.Description;
            product.Price = pro.Price;
            product.CurrentCategoryID = pro.CurrentCategoryID;
            product.CurrentSupplierID = pro.CurrentSupplierID;


            if (product == null)
            {
                result.Message = "NotFound Product";
            }
            ProductRepo.Update(product);
            UnitOfWork.Save();
            return result;
        }
        [Route("Delete/{id}")]
        public ResultViewModel deleteProduct(int id)
        {
            result.Message = "Deleted Product";

            result.Data = ProductRepo.GetByID(id);
            ProductRepo.Remove(ProductRepo.GetByID(id));
            UnitOfWork.Save();
            return result;
        }

        [HttpGet("stores")]


        public ResultViewModel GetStores()
        {
            result.Message = "All stores";
            result.Data = StoreRepo.Get();
            return result;
        }

        //[HttpGet("storesById/{id}")]
        //public ResultViewModel storesById(int id)
        //{
        //    Store store = StoreRepo.GetByID(id);
        //    if (store == null)
        //    {
        //        result.ISuccessed = false;
        //        return result;
        //    }
        //    result.Message = " Store By ID";
        //    result.Data = StoreRepo.GetByID(id);
        //    return result;
        //}

        //[HttpPost("addStore")]
        //public ResultViewModel addStore(StoreViewModel sto)
        //{
        //    result.Message = "Add Store";
        //    var store = new Store();
        //    store.Name = sto.Name;
        //    store.Address = sto.Address;
        //    store.Phone = sto.Phone;


        //    StoreRepo.Add(store);
        //    UnitOfWork.Save();
        //    result.Data = store;

        //    return result;

        //}
        //[HttpPut("editStore")]
        //public ResultViewModel editStore(int id, StoreViewModel sto)
        //{
        //    var store = StoreRepo.GetByID(id);
        //    result.Data = ProductRepo.GetByID(id).ToViewModel();

        //    store.Name = sto.Name;
        //    store.Address = sto.Address;
        //    store.Phone = sto.Phone;

        //    if (store == null)
        //    {
        //        result.Message = "NotFound Store";
        //    }
        //    result.Data = store;
        //    StoreRepo.Update(store);
        //    UnitOfWork.Save();
        //    return result;
        //}


        //[HttpDelete("deleteStore/{id}")]
        //public ResultViewModel deleteStore(int id)
        //{
        //    result.Data = StoreRepo.GetByID(id);
        //    StoreRepo.Remove(StoreRepo.GetByID(id));
        //    UnitOfWork.Save();
        //    result.Message = "Store Deleted";
        //    return result;
        //}


        [HttpGet("category")]
        public ResultViewModel Getcategory()
        {

            result.Message = "All category";
            result.Data = CategoryRepo.Get();
            return result;
        }
        [HttpPost("Addcategory")]
        public ResultViewModel Addcategory(Category category)
        {
            result.Message = "Add category";
            result.Data = category;
            CategoryRepo.Add(category);
            UnitOfWork.Save();
            return result;
        }

        [Route("Deletecategory/{id}")]
        public ResultViewModel Deletecategory(int id)
        {

            result.Message = " Delete category";
            result.Data = CategoryRepo.GetByID(id);
            IEnumerable<Product> products = UnitOfWork.context().Products.Where(p => p.CurrentCategoryID == id);
            UnitOfWork.context().Products.RemoveRange(products);
            UnitOfWork.context().Categories.Remove(CategoryRepo.GetByID(id));
            UnitOfWork.context().SaveChanges();
            return result;
        }
        [HttpPut("Updatecategory")]
        public ResultViewModel UpdateCategory(Category category)
        {

            result.Message = " Delete category";
            result.Data = category;
            CategoryRepo.Update(category);
            UnitOfWork.Save();
            return result;
        }

        [Route("CategoryID/{id}")]
        public ResultViewModel CategoryID(int id)
        {

            result.Message = " CategoryID";
            result.Data = CategoryRepo.GetByID(id);
            
            return result;
        }

        [HttpPost("addangularimages")]
        public ResultViewModel addangularimages(Images[] image)
        {
            var res = image;
            result.Message = "Add Images for Product";
            Product pro = ProductRepo.Get().OrderByDescending(o=>o.ID).Take(1).FirstOrDefault();
            if (pro != null)
            {
                for(int i = 0; i < image.Length; i++)
                {
                    image[i].product = pro;
                    UnitOfWork.context().Images.Add(image[i]);
                    pro.Product_Images.Add(image[i]);

                }
                UnitOfWork.Save();
                result.Data = image;

            }
            else
            {
                result.ISuccessed = false;
                result.Message = "not Added";
            }

            return result;
        }
        [HttpPost("addangularProduct")]
        public ResultViewModel addangularProduct(Product product)
        {
            //StoreProduct sp = new StoreProduct();
            var res = product;
            result.Message = "Add Product";


            Category Cat = CategoryRepo.Get().Where(c => c.ID == product.CurrentCategoryID).FirstOrDefault();
            if (Cat != null)
            {
                product.category = Cat;
            }
            User saller = Context.Users.Where(s => s.Id == product.CurrentSupplierID).FirstOrDefault();
            if (saller != null)
            {
                product.supplier = saller;
            }

            ProductRepo.Add(product);
            UnitOfWork.Save();
            result.Data = UnitOfWork.context().Products.OrderByDescending(o=>o.ID).FirstOrDefaultAsync();

            return result;
        }

    }
}
     