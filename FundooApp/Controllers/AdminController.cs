using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Model.AccountModels;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly  IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost]
        public async Task<IActionResult>Register(RegistrationModel registrationModel)
        {
            try
            {
                var result=await this.adminBL.Register(registrationModel);
                var message = string.Empty;
                bool success = false;
                if (result)
                {
                    success = true;
                    message = "User Registered As admin";
                    return this.Ok(new { success, message });
                }
                else
                {
                    success = false;
                    message = "something went wrong";
                    return this.BadRequest(new { success, message });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult>Login(LoginModel loginModel)
        {
            try
            {
                var data = await this.adminBL.Login(loginModel);
                bool success = false;
                var message = string.Empty;
                if (data != null)
                {
                    success = true;
                    message = "Login successfull";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Login failed";
                    return this.BadRequest(new { success, message });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}