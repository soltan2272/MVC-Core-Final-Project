using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reposotries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Userr;

namespace Final_Project.Controllers.Admin
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IUserRepository UserRepository;
        ResultViewModel result = new ResultViewModel();
        public AdminController(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            UserRepository = userRepository;

        }


        [HttpPost("addadmin")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signupModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await UserRepository.SignUp(signupModel);
            if (!result.IsAuthenticated)
                return Ok(result);

            var adminRole = new AddRoleModel
            {
                UserID = result.User_ID,
                Role = "Admin"
            };

            await AddRole(adminRole);
            return Ok(result);

        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await UserRepository.Login(loginModel);
            if (!result.IsAuthenticated)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("addrole")]
        private async Task<IActionResult> AddRole(AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await UserRepository.AddRole(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }

        [HttpGet("getadmins")]
        public async Task<dynamic> GetAllUsers()
        {

            var res = await UserRepository.GetAdminsAsync();
            result.Data = res;
            result.Message = "Succedd";
            result.ISuccessed = true;
            return result;
        }

        [HttpGet("getadmin/{id}")]
        public async Task<ResultViewModel> GetUserByID(int id)
        {
            ViewUser res = await UserRepository.GetUserBYIDAsync(id);
            // User view = usr.ToModel();
            result.Data = res;
            result.Message = "Succedd";
            result.ISuccessed = true;
            return result;
        }

        [HttpDelete("deleteadmin/{id}")]
        public async Task<dynamic> DeleteUser(int id)
        {
            var res = await UserRepository.DeleteUser(id);
            result.ISuccessed = true;
            result.Data = res;
            result.Message = "Deleted Successfully";
            return result;
        }
    }

}
