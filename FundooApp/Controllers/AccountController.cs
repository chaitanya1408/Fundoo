using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CommonLayer.Model.Account;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountBL accountBL;
        public AccountController(IAccountBL accountBL)
        {
            this.accountBL = accountBL;
        }

        [HttpPost]
        [Route("Register")]
       
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
           
            var result = await this.accountBL.Register(registrationModel);
            bool success = false;
            var message = string.Empty;

            
            if (result)
            {
                success = true;
                message = "Registration Succeffully Completed";
                return this.Ok(new { success, message });
            }
            else
            {
                success = false;
                message = "Registration Failed";
                return this.BadRequest(new { success, message });
            }
        }
        [HttpPost]
        [Route("Login")]
        
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            bool success = false;
            var message = string.Empty;

            // checking login information
            var data = await this.accountBL.Login(loginModel);

            // check whether user get login or not
            if (data != null)
            {
                success = true;
                message = "Login Successfull";

                // generate the token
                var token = await this.accountBL.GenerateToken(data);
                return this.Ok(new { success, message, token, data });
            }
            else
            {
                success = false;
                message = "Login Failed";
                return this.BadRequest(new { success, message });
            }
        }
        [HttpPost]
        [Route("ForgetPassword")]
        ////Post: /api/Account/ForgetPassword
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            bool success = false;
            var message = string.Empty;

            // geting the token
            var token = await this.accountBL.ForgetPassword(forgetPasswordModel);

            // chek whether token is generated or not
            if (token)
            {
                success = true;
                message = "Token is sent to your email id..Please Check your mail";
                return this.Ok(new { success, message });
            }
            else
            {
                success = false;
                message = "Invalid emailID";
                return this.BadRequest(new { success, message });
            }
        }
        [HttpPut]
        [Route("ResetPassword")]
        
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
          
            var token = await this.accountBL.ResetPassword(resetPasswordModel);
            string message = string.Empty;
            bool success = false;

           
            if (token)
            {
                success = true;
                message = "Password changed successfully ";
                return this.Ok(new { success, message });
            }
            else
            {
                success = false;
                message = "Password Reset Failed";
                return this.BadRequest(new { success, message });
            }
        }
    }
}