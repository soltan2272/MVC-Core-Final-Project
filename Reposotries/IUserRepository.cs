using Data;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Userr;

namespace Reposotries
{
    public interface IUserRepository
    {
        Task<AuthModel> SignUp(SignUpModel signUpModel);

        Task<AuthModel> Login(LoginModel loginModel);

        Task<string> AddRole(AddRoleModel Model);
        Task<List<ViewUser>> GetUsersAsync();

        Task<List<ViewUser>> GetSellersAsync();

        Task<List<ViewUser>> GetAdminsAsync();

        Task<ViewUser> GetUserBYIDAsync(int id);

        Task<List<User>> DeleteUser(int id);
        Task<string> ChangePassword(ChangePassword _user);
        Task<string> UserEditProfile(EditProfile model);
    }
}
