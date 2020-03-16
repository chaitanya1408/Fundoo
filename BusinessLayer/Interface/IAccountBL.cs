using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.Model;

using CommonLayer.Model.AccountModels;

namespace BusinessLayer.Interface
{
    public interface IAccountBL
    {
        Task<bool> Register(RegistrationModel registrationModel);
        Task<bool> ForgetPassword(ForgetPasswordModel forgetPasswordModel);
        Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel);
        Task<string> GenerateToken(AccountResponse accountResponse);
        Task<AccountResponse> Login(LoginModel loginModel);

    }
}
