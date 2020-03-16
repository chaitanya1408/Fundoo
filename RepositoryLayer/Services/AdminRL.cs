using CommonLayer.Model;
using CommonLayer.Model.AccountModels;
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
        private readonly ApplicationSetting applicationSetting;
        private readonly AuthenticationContext authenticationContext;

        public AdminRL(ApplicationSetting applicationSetting,AuthenticationContext authenticationContext)
        {
            this.authenticationContext = authenticationContext;
            this.applicationSetting = applicationSetting;
        }

        public IList<AccountResponse> GetAllUser(AdminModel adminModel)
        {
            var data = this.authenticationContext.Note.Where(s => s.UserID == s.UserID);
            var list = new List<AccountResponse>();
            if (data != null)
            {
                if (data != null)
                {
                    
                    foreach (var note in data)
                    {
                        AdminModel notes = this.GetNoteResponse(data, note); ;

                        list.Add(note);
                    }
                  
                    return list;
                }
                else
                {
                    return null;
                }
            }
        }

        private AdminModel GetNoteResponse(object userID, object note)
        {
            throw new NotImplementedException();
        }
    }
}
