using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        Project_Context Context;
        ResultViewModel result = new ResultViewModel();

        public ReportController(Project_Context context)
        {
            Context = context;     
        }


        [HttpGet("Statistics")]
        public ResultViewModel GetforUser()
        {
            Statistics statistics = new Statistics();
            statistics.Admins = Context.UserRoles.Where(a => a.RoleId == 2).Count();
            statistics.Sallers = Context.UserRoles.Where(a => a.RoleId == 3).Count();
            statistics.Users = Context.UserRoles.Where(a => a.RoleId == 1).Count();
            statistics.Products = Context.Products.Count();
            statistics.Offers = Context.Offers.Count();
            statistics.Orders = Context.Orders.Count();
            statistics.Feadbacks = Context.Feedbacks.Count();
            var totalPrice = Context.Orders.Select(e => e.TotalPrice).ToArray();
            double earning = 0; ;
            for (int i = 0; i < totalPrice.Count(); i++)
            {
               earning += totalPrice[i];
            }
            statistics.earnings = earning * .14;
            result.Message = "All Statistics";
            result.Data = statistics;

            return result;
        }


    }
}
