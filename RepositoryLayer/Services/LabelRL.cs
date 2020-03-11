using CommonLayer.Model;
using CommonLayer.Model.Request;
using CommonLayer.Model.Response;
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
    public class LabelRL : ILabelRL
    {
        private AuthenticationContext authenticationContext;

       
        public LabelRL(AuthenticationContext authenticationContext)
        {
            this.authenticationContext = authenticationContext;
        }

        
        public async Task<bool> CreateLabel(LabelRequest labelRequest, string userID)
        {
            try
            {
               
                if (labelRequest != null)
                {
                   
                    var data = new LabelModel()
                    {
                        UserID = userID,
                        Label = labelRequest.Label,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };

                    this.authenticationContext.Label.Add(data);

                    
                    await this.authenticationContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelRequest">The label request.</param>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the info of label
        /// </returns>
        /// <exception cref="Exception"> exception message</exception>
        public async Task<LabelResponse> UpdateLabel(LabelRequest labelRequest, int labelID, string userID)
        {
            try
            {
                // get the label info from label tabel
                
                var label = this.authenticationContext.Label.Where(s => s.LabelID == labelID && s.UserID == userID).FirstOrDefault();
                // check whether label data is null or not
                if (label != null)
                {
                    // set the current date and time for modified date property
                    label.ModifiedDate = DateTime.Now;

                    // check whether user enter label name or not
                    if (labelRequest.Label != null && labelRequest.Label != string.Empty)
                    {
                        label.Label = labelRequest.Label;
                    }

                    // update the label name
                    this.authenticationContext.Label.Update(label);
                    await this.authenticationContext.SaveChangesAsync();

                    var data = new LabelResponse()
                    {
                        ID = label.LabelID,
                        Label = label.Label,
                    };
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns message indicating operation is done or not
        /// </returns>
        /// <exception cref="Exception"> exception message</exception>
        public async Task<bool> DeleteLabel(int labelID, string userID)
        {
            try
            {
                // get the label info from label tabel
                var label = this.authenticationContext.Label.Where(s => s.LabelID == labelID && s.UserID == userID).FirstOrDefault();

                // check whether label is present in tabel or not
                if (label != null)
                {
                    // delete the label entry from tabel
                    this.authenticationContext.Label.Remove(label);
                    await this.authenticationContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Displays the labels.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the list of label
        /// </returns>
        /// <exception cref="Exception"> exception message</exception>
        public IList<LabelResponse> DisplayLabels(string userID)
        {
            try
            {
                // get the labels data from tabel
                var data = this.authenticationContext.Label.Where(s => s.UserID == userID);

                var list = new List<LabelResponse>();

                if (data != null)
                {
                    foreach (var label in data)
                    {
                        var labels = new LabelResponse()
                        {
                            ID = label.LabelID,
                            Label = label.Label,
                        };

                        list.Add(labels);
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
