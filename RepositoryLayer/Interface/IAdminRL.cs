using CommonLayer.Model;
using CommonLayer.Model.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAdminRL
    {
        Task<bool> Register(RegistrationModel registrationModel);

        Task<AccountResponse> Login(LoginModel loginModel);
        //IList<AccountResponse> GetAllUser(AdminModel adminModel);
    }
}
