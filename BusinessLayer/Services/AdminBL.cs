using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.Model.AccountModels;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AdminBL:IAdminBL
    {
        public readonly IAdminRL adminRL;

        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }
        public Task<bool> Register(RegistrationModel registrationModel)
        {
            try
            {
                if (registrationModel != null)
                {
                    return this.adminRL.Register(registrationModel);
                }
                else
                {
                    throw new Exception("Data Required");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<AccountResponse>Login(LoginModel loginModel)
        {
            try
            {
                if (loginModel != null)
                {
                    return this.adminRL.Login(loginModel);
                }
                else
                {
                    throw new Exception("Enter valid EmailId and Pasword");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        //public IList<AccountResponse> GetAllUser(AdminModel adminModel)
        //{
        //    try
        //    {
        //        if (AdminModel != null)
        //        {

        //        }
        //    }
        //}
    }
}
