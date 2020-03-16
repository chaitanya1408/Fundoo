using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IAdminBL adminBL;

        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpGet]
        public async Task<IActionResult>GetAllUser(AdminModel adminModel)
        {
            var result = this.adminBL.GetAllUser(adminModel);
            bool success = false;
            var message = string.Empty;
            if (result != null)
            {
                success = true;
                message = "Displaying All Users";
                return this.Ok(new { success, message, result });
            }
            else
            {
                success = false;
                message = "something went wrong";
                return this.BadRequest(new { success, message });
            }
        }
    }
}