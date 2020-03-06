using CommonLayer.Model;
using CommonLayer.Model.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAccountRL
    {
        Task<bool> Register(RegistrationModel registrationModel);

       
        Task<AccountResponse> LogIn(LoginModel loginModel);

       
        Task<bool> ForgetPassword(ForgetPasswordModel forgetPasswordModel);

      
        Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel);

       
        Task<string> GenerateToken(AccountResponse accountResponse);

    }
}
