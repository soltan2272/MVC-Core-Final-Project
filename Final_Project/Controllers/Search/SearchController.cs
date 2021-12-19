using Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Reposotries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Controllers
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Route("api/[controller]")]

    public class SearchController : ControllerBase
    {
        IGenericRepostory<Product> ProductRepo;
        IUnitOfWork UnitOfWork;
        Project_Context Context;

        ResultViewModel result = new ResultViewModel();
        public SearchController(IUnitOfWork unitOfWork, Project_Context context)
        {
            UnitOfWork = unitOfWork;
            ProductRepo = UnitOfWork.GetProductRepo();
            Context = context;
        }

        [HttpGet("Name/{Name}")]
        public ResultViewModel Name(string Name)
        {
            var res = ProductRepo.Get().Where(p => p.Name.Contains(Name)).Select(p => p.ToViewModel());
            if (res != null)
            {
                result.Message = "All Products have Name: " + Name;
                result.Data = res;
            }
            else
            {
                result.ISuccessed = false;
                result.Message = " not found ";
            }
            return result;
        }

        [HttpGet("PriceLessThan/{price}")]
        public ResultViewModel PriceLessThan(int price)
        {
            var res = ProductRepo.Get().Where(p => p.Price<=price).Select(p => p.ToViewModel());
            if (res != null)
            {
                result.Message = "Products Less Than " + price;
                result.Data = res;
            }
            else
            {
                result.ISuccessed = false;
                result.Message = "not found";
            }
            return result;
        }

        [HttpGet("Pricerange/{price}/{price2}")]
        public ResultViewModel PriceLessThan(int price , int price2 )
        {
            var res = ProductRepo.Get().Where(p => p.Price >= price&&p.Price<=price2).Select(p => p.ToViewModel());
            if (res != null)
            {
                result.Message = "Products Less Than " + price;
                result.Data = res;
            }
            else
            {
                result.ISuccessed = false;
                result.Message = "not found";
            }
            return result;
        }

        [HttpGet("PriceMoreThan/{price}")]
        public ResultViewModel PriceMoreThan(int price)
        {
           var res = ProductRepo.Get().Where(p => p.Price >= price).Select(p => p.ToViewModel());

            if (res != null)
            {
                result.Message = "Products Less Than " + price;
                result.Data = res;
            }
            else
            {
                result.ISuccessed = false;
                result.Message = "not found";
            }
            return result;
        }

        [HttpGet("Rate/{Rate}")]
        public ResultViewModel Rate(int Rate)
        {
            if(Rate<=5&& Rate >= 0)
            {
                result.Message = "Products Where Rate = " + Rate;
                result.Data = ProductRepo.Get().Where(p => p.Rate <= Rate).Select(p => p.ToViewModel());

            }
            else
            {
                result.ISuccessed = false;
                result.Message = "error not valied ";

            }
            return result;
        }

        [HttpGet("Category/{Category}")]
        public ResultViewModel Category(int Category)
        {
            var res = result.Data =UnitOfWork.context().Products.Where(p => p.CurrentCategoryID == Category).Select(p => new
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
            if (res != null)
            {
                result.Message = "Products By Category Name ";
                result.Data = res;
            }
            else
            {
                result.ISuccessed = false;
                result.Message = "not found";
            }
            return result;
        }
        [HttpGet("Category/{Category}/{search}")]
        public ResultViewModel CategorySearch(int Category,string search)
        {
            var res = ProductRepo.Get().Where(p => p.CurrentCategoryID == Category).Select(p => p.ToViewModel()).Where(pro=>pro.Name.Contains(search));
            if (res != null)
            {
                result.Message = "Products By Category Name ";
                result.Data = res;
            }
            else
            {
                result.ISuccessed = false;
                result.Message = "not found";
            }
            return result;
        }

        [HttpGet("Seller/{Seller}")]
        public ResultViewModel Seller(int Seller)
        {
            var res = result.Data = Context.Products.Where(p => p.CurrentSupplierID == Seller).Select(p => new
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

            if (res != null)
            {
                result.Message = "Products By Seller Name ";
                result.Data = res;
            }
            else
            {
                result.ISuccessed = false;
                result.Message = "not found";
            }
            return result;
        }
        [HttpGet("TopRate")]
        public ResultViewModel TopRate()
        {
            var res = result.Data = Context.Products.Select(p => new
            {
                p.ID,
                p.Name,
                p.Price,
                p.ProductOffers,
                p.Rate,
                p.Product_Images,
            }).OrderByDescending(o => o.Rate).Take(8); ;

            if (res != null)
            {
                result.Message = "Products By Seller Name ";
                result.Data = res;
            }
            else
            {
                result.ISuccessed = false;
                result.Message = "not found";
            }
            return result;
        }
        [HttpGet("CheepProducts")]
        public ResultViewModel CheepProducts()
        {
            var res = result.Data = Context.Products.Select(p => new
            {
                p.ID,
                p.Name,
                p.Price,
                p.ProductOffers,
                p.Rate,
                p.Product_Images,
            }).OrderBy(o => o.Price).Take(8); ;

            if (res != null)
            {
                result.Message = "Products By Seller Name ";
                result.Data = res;
            }
            else
            {
                result.ISuccessed = false;
                result.Message = "not found";
            }
            return result;
        }

        [HttpGet("NewProducts")]
        public ResultViewModel NewProducts()
        {
            var res = result.Data = Context.Products.Select(p => new
            {
                p.ID,
                p.Name,
                p.Price,
                p.ProductOffers,
                p.Rate,
                p.Product_Images,
            }).OrderBy(o => o.ID).Take(8); ;

            if (res != null)
            {
                result.Message = "Products By Seller Name ";
                result.Data = res;
            }
            else
            {
                result.ISuccessed = false;
                result.Message = "not found";
            }
            return result;
        }
    }
}
