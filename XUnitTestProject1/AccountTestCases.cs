using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.Model.AccountModels;
using FundooApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RepositoryLayer.Interface;
using System;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class AccountTestCases
    {
        AccountController accountController;

        private readonly IAccountBL accountBL;

        public AccountTestCases()
        {
            var repository = new Mock<IAccountRL>();
            this.accountBL = new AccountBL(repository.Object);
            accountController = new AccountController(this.accountBL);
        }

        [Fact]
        public async Task TestRegistrationForBadRequest()
        {
            RegistrationModel data = new RegistrationModel()
            {
                FirstName = "Abc",
                LastName = "Abc",
                Username = "abc",
                EmailId = "abcgmail.com",
                ServiceType = "Basic",
                PassWord = ""
            };

            var result = await accountController.Register(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task TestLoginForBadRequest()
        {
           LoginModel data=new LoginModel()
            {
                EmailId = "cvvaidya14@gmail.com",
                Password = ""
            };
            var result = await accountController.Login(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task TestLoginForPassword()
        {
            LoginModel data = new LoginModel()
            {
                EmailId = "cvvaidya14@gmail.com"
            };

            accountController.ModelState.AddModelError("Password", "Required");
            var result = await accountController.Login(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task TestForForgotPasswordBadRequest()
        {
            ForgetPasswordModel data = new ForgetPasswordModel()
            {
                EmailID="cvvaidya14@gmail.com"
            };
            accountController.ModelState.AddModelError("Password", "Required");
            var result = await accountController.ForgetPassword(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task TestForForgotPasswordOkresult()
        {
            ForgetPasswordModel data = new ForgetPasswordModel()
            {
                EmailID = "cvvaidya14@gmail.com"
            };
            var result = await accountController.ForgetPassword(data);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestForForgotPasswordNullRequest()
        {
            ForgetPasswordModel data = new ForgetPasswordModel()
            {

            };
            accountController.ModelState.AddModelError("Email", "Required");
            var result = await accountController.ForgetPassword(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task TestForResetPasswordBadRequest()
        {
            ResetPasswordModel data=new ResetPasswordModel()
            {
                Password = "ab"
            };
            accountController.ModelState.AddModelError("Token", "Required");
            var result = await accountController.ResetPassword(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task TestForNullResetPassWordReset()
        {
            ResetPasswordModel data = new ResetPasswordModel()
            {
                Password = ""
            };
            accountController.ModelState.AddModelError("Password", "Required");
            var result = await accountController.ResetPassword(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task TestForResetPasswordSuceess()
        {
            ResetPasswordModel data = new ResetPasswordModel()
            {
                Password = "Abc@123"
            };
            var result = await accountController.ResetPassword(data);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
