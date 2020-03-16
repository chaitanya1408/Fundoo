using CommonLayer.Model;
using CommonLayer.Model.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAdminBL
    {
        Task<AccountResponse> GetAllUser(AdminModel adminModel);
    }
}

