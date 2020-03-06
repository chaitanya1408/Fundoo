using CommonLayer.Model.Account;
using Microsoft.AspNetCore.Identity;
using RepositoryLayer.Interface;
using CommonLayer.Model;
using System.Threading.Tasks;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

using System.IdentityModel.Tokens.Jwt;


using Microsoft.Extensions.Options;
using System.Linq;
using CommonLayer.MSMQ;

namespace RepositoryLayer.Services
{
    public class AccountRL : IAccountRL
    {
        private readonly UserManager<ApplicationModel> userManager;
        private readonly SignInManager<ApplicationModel> signInManager;
        private readonly ApplicationSetting applicationSetting;
        public AccountRL(UserManager<ApplicationModel> userManager, SignInManager<ApplicationModel> signInManager,IOptions<ApplicationSetting> appSetting)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationSetting = appSetting.Value;
        }

        public async Task<bool> ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            var user = await this.userManager.FindByEmailAsync(forgetPasswordModel.EmailID);

            MSMQSender msmq = new MSMQSender();

            if (user != null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                   {
                      new Claim("EmailID", user.Email.ToString())
                   }),

                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.applicationSetting.JWTSecret)), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> GenerateToken(AccountResponse accountResponse)
        {
            try 
            {
                var user = await this.userManager.FindByEmailAsync(accountResponse.EmailID);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", accountResponse.UserID.ToString()),
                        new Claim("EmailID", accountResponse.EmailID)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.applicationSetting.JWTSecret)), SecurityAlgorithms.HmacSha256)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return token;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
       
        public async Task<AccountResponse> LogIn(LoginModel loginModel)
        {
            try
            {
                var user = await this.userManager.FindByEmailAsync(loginModel.EmailId);

                var userPassword = await this.userManager.CheckPasswordAsync(user, loginModel.Password);
                if(user != null)
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

        public async Task<bool> Register(RegistrationModel registrationModel)
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
                
                var result = await this.userManager.CreateAsync(data, registrationModel.PassWord);

                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            var token = new JwtSecurityToken(jwtEncodedString: resetPasswordModel.Token);
            var email = token.Claims.First(c => c.Type == "EmailID").Value;
            var user = await this.userManager.FindByEmailAsync(email);
            if (email != null)
            {
                var resetToken = await this.userManager.GeneratePasswordResetTokenAsync(user);
                var result = await this.userManager.ResetPasswordAsync(user, resetToken, resetPasswordModel.Password);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
