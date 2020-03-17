using CommonLayer.Model;
using CommonLayer.Model.AccountModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class AdminRL:IAdminRL
    {
        private readonly UserManager<ApplicationModel> userManager;
        private readonly SignInManager<ApplicationModel> signInManager;
        private readonly ApplicationSetting applicationSetting;
        private AuthenticationContext authenticationContext;

        public AdminRL(UserManager<ApplicationModel> userManager, SignInManager<ApplicationModel> signInManager, IOptions<ApplicationSetting> appSetting,AuthenticationContext authenticationContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationSetting = appSetting.Value;
            this.authenticationContext = authenticationContext;
        }

       public async Task<bool>Register(RegistrationModel registrationModel)
        {
            try
            {
                var user = await this.userManager.FindByEmailAsync(registrationModel.EmailId);
                if (user == null)
                {
                    var data = new ApplicationModel()
                    {
                        FirstName = registrationModel.FirstName,
                        LastName = registrationModel.LastName,
                        UserName = registrationModel.Username,
                        Email = registrationModel.EmailId,
                        UserType = registrationModel.UserType,
                        ServiceType = registrationModel.ServiceType
                    };
                    if (data.UserType == "Admin")
                    {
                        var datadmin = new ApplicationModel()
                        {
                            FirstName = registrationModel.FirstName,
                            LastName = registrationModel.LastName,
                            UserName = registrationModel.Username,
                            Email = registrationModel.EmailId,
                            UserType = "Admin",
                            ServiceType = "advanced"
                        };
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    throw new Exception("User is already registered");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
       }
        
        public async Task<AccountResponse>Login(LoginModel loginModel)
        {
            try
            {
                var user = await this.userManager.FindByEmailAsync(loginModel.EmailId);
                var password = await this.userManager.CheckPasswordAsync(user,loginModel.Password);
                if (user != null)
                {
                    var data = new AccountResponse()
                    {
                        UserID = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailID = user.Email,
                        ServiceType = user.ServiceType,
                        UserName = user.UserName,
                        UserType = user.UserType
                    };
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IList<AccountResponse> GetAllUser(AdminModel adminModel)
        {
            try
            {
                var user = this.userManager.FindByNameAsync(adminModel.UserType);
                var list = new List<AccountResponse>();
                if (user != null)
                {
                    return list;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
