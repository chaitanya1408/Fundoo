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
        public async Task<AccountResponse> GetAllUser(AdminModel adminModel)
        {
            try
            {
                if (adminModel != null)
                {
                    return await this.adminRL.GetAllUser(adminModel);
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
