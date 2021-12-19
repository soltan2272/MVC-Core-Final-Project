using Microsoft.AspNetCore.Cors;

﻿using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Models;
using Reposotries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.User;
using ViewModels.Userr;

namespace Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {
     
        IUserRepository UserRepository;
        IGenericRepostory<Order> OrderRepo;
        IGenericRepostory<Feedback> FeedbackRepo;

        Project_Context Context;

        IUnitOfWork UnitOfWork;
        IGenericRepostory<ContactUs> ContactRepo;

        ResultViewModel result = new ResultViewModel();
        
        public UserController(IUserRepository userRepository,
            IUnitOfWork unitOfWork, Project_Context context, IGenericRepostory<ContactUs> contactRepo)
        {
            UnitOfWork = unitOfWork;
            UserRepository = userRepository;
            OrderRepo = UnitOfWork.GetOrderRepo();
            FeedbackRepo = UnitOfWork.GetFeedbackRepo();
            ContactRepo = UnitOfWork.GetContactRepo();

            Context = context;
            
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody]SignUpModel signupModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await UserRepository.SignUp(signupModel);
            if(!result.IsAuthenticated)
                return Ok(result);

            return Ok(result);

        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await UserRepository.Login(loginModel);
            if (!result.IsAuthenticated)
                return Ok(result);
            return Ok(result);
        }

        [HttpGet("")]
        public ResultViewModel Get()
        {

            result.Message = "All Orders";
            result.Data =UnitOfWork.context().Orders.Select(o=>new {
                                                                    o.ID,
                                                                    o.Order_Date,
                                                                    o.Quantity,
                                                                    o.Governmate,
                                                                    o.Delivery_Status,
                                                                    o.User,
                                                                    o.TotalPrice,
                                                                    o.productOrders});
            return result;
        }

        [HttpGet("getusers")]
        public async Task<dynamic> GetAllUsers()
        {

            var res = await UserRepository.GetUsersAsync();
            result.Data = res;
            result.Message = "Succedd";
            result.ISuccessed = true;
            return result;
        }

        [HttpGet("getuser/{id}")]
        public async Task<ResultViewModel> GetUserByID(int id)
        {
            ViewUser res = await UserRepository.GetUserBYIDAsync(id);
           // User view = usr.ToModel();
           result.Data = res;
            result.Message = "Succedd";
            result.ISuccessed = true;
            return result;
        }

        [HttpDelete("deleteuser/{id}")]
        public async Task<dynamic> DeleteUser(int id)
        {
            var res = await UserRepository.DeleteUser(id);
            result.ISuccessed = true;
            result.Data = res;
            result.Message = "Deleted Successfully";
            return result;
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> Changepassword([FromBody] ChangePassword changePassword)
        {
            if (!ModelState.IsValid)
                return Ok(ModelState);

            var result = await UserRepository.ChangePassword(changePassword);
            return Ok(result);
        }

        [HttpPost("edituser")]
        public async Task<IActionResult> editUser([FromBody] EditProfile editprofile)
        {
            if (!ModelState.IsValid)
                return Ok(ModelState);

            var result = await UserRepository.UserEditProfile(editprofile);
            return Ok(result);
        }

        [HttpDelete("deleteorder/{id}")]
        public ResultViewModel DeleteOrder(int id)
        {
            result.Message = " Order Deleted";
            result.Data = OrderRepo.GetByID(id);
            OrderRepo.Remove(OrderRepo.GetByID(id));
            UnitOfWork.Save();

            return result;

        }
        [Route("OrderID/{id}")]
        public ResultViewModel OrderID(int id)
        {
            result.Message = " OrderID";
            result.Data = UnitOfWork.context().Orders.Where(o=>o.ID==id).Select(o=>new { 
            o.ID,
            o.Order_Date,
            o.productOrders,
            o.Quantity,
            o.TotalPrice,
            o.User,
            o.Delivery_Status,
            o.Governmate}).FirstOrDefault();
            return result;
        }

        [HttpPatch("Updateorder")]
        public ResultViewModel Updateorder(Order order)
        {

            result.Message = " order";
            UnitOfWork.context().Orders.Update(order);
            UnitOfWork.Save();
            result.Data = order;

            return result;
        }
      

        [HttpPost("addfeedback")]
        public ResultViewModel AddFeedback(Feedback feedback)
        {
            result.Message = "Add User Feedback";

            FeedbackRepo.Add(feedback);
            UnitOfWork.Save();
            result.Data = feedback;

            return result;

        }
        [HttpPost("addContactUs")]
        public ResultViewModel AddContactUs(ContactUs contactUs)
        {
            result.Message = "Add  ContactUs";

            ContactRepo.Add(contactUs);
            UnitOfWork.Save();
            result.Data = contactUs;

            return result;

        }
        [HttpDelete("deleteContactUs")]
        public ResultViewModel DeleteContactUs(int id)
        {
            result.Message = " ContactUs Deleted";
            result.Data = ContactRepo.GetByID(id);
            ContactRepo.Remove(ContactRepo.GetByID(id));
            UnitOfWork.Save();

            return result;

        }
        [HttpGet("getContactUs")]
        public ResultViewModel GetContactUs()
        {
            result.Message = " get ContactUs";
            result.Data = ContactRepo.Get();
            UnitOfWork.Save();
            return result;
        }
       
      

        [HttpPost("addorder")]
        public ResultViewModel addorder(Order order)
        {

            result.Message = " order";
            result.Data = order;
            OrderRepo.Add(order);
            UnitOfWork.Save();
            return result;
        }

        [HttpGet("getOrder/{id}")]
        public ResultViewModel getOrder(int id)
        {

            result.Message = " order";
            result.Data = OrderRepo.GetByID(id);
            return result;
        }
        [HttpGet("allOrders")]
        public ResultViewModel allOrders()
        {

            result.Message = " order";
            result.Data = UnitOfWork.context().Orders.Select(o => new
            {
                o.ID,
                o.Order_Date,
                o.Quantity,
                o.TotalPrice,
                o.User,
                o.Delivery_Status,
                o.Governmate,
                o.CurrentUserID
            }
           );
            return result;
        }

    }

}
