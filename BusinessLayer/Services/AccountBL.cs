using BusinessLayer.Interface;
using CommonLayer.Model.AccountModels;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{

    public class AccountBL:IAccountBL
    {
        private readonly IAccountRL accountRL;
        public AccountBL(IAccountRL accountRl)
        {
            this.accountRL = accountRl;
        }
        public async Task<bool> Register(RegistrationModel registrationModel)
        {
            try
            {
                // check whether all properties entered by user have some value or not
                if (registrationModel != null)
                {
                    // return true if registaration is successfull
                    return await this.accountRL.Register(registrationModel);
                }
                else
                {
                    // otherwise throw exception
                    throw new Exception("Data Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public async Task<bool>ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {
                if (forgetPasswordModel != null)
                {
                    var result = await this.accountRL.ForgetPassword(forgetPasswordModel);
                    return result;
                }
                else
                {
                    throw new Exception("Invalid Input");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        public async Task<bool>ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                if (resetPasswordModel != null)
                {
                    var result = await this.accountRL.ResetPassword(resetPasswordModel);
                    
                    return result;
                }
                else
                {
                    throw new Exception("Invalid Email Id");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<string> GenerateToken(AccountResponse accountResponse)
        {
            try
            {
                if (accountResponse != null)
                {
                    var result = await this.accountRL.GenerateToken(accountResponse);
                    return result;
                }
                else
                {
                    throw new Exception("Invalid Account");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<AccountResponse> Login(LoginModel loginModel)
        {
            try
            {
               
                if (loginModel != null)
                {
                   
                    var result = await this.accountRL.LogIn(loginModel);
                    return result;
                }
                else
                {
                    throw new Exception("EmailId and Password is Requirred");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

    }
}
