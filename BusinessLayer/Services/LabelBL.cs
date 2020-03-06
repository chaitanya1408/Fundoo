using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.Model.Request;
using CommonLayer.Model.Response;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelBL"/> class.
        /// </summary>
        /// <param name="labelRL"> reference of repository layer label class.</param>
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="labelRequest">The label request.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns true or false indicating operation is successful or not</returns>
        /// <exception cref="Exception">
        /// Data Required
        /// or
        /// </exception>
        public async Task<bool> CreateLabel(LabelRequest labelRequest, string userID)
        {
            try
            {
                if (labelRequest != null)
                {
                    return await this.labelRL.CreateLabel(labelRequest, userID);
                }
                else
                {
                    throw new Exception("Label Data Required");
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
        /// <exception cref="Exception">
        /// Please enter LabelID
        /// or
        /// </exception>
        public async Task<LabelResponse> UpdateLabel(LabelRequest labelRequest, int labelID, string userID)
        {
            try
            {
                if (labelID != 0)
                {
                    return await this.labelRL.UpdateLabel(labelRequest, labelID, userID);
                }
                else
                {
                    throw new Exception("Please enter LabelID");
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
        /// <exception cref="Exception">
        /// Please enter label ID
        /// or
        /// </exception>
        public async Task<bool> DeleteLabel(int labelID, string userID)
        {
            try
            {
                if (labelID != 0)
                {
                    return await this.labelRL.DeleteLabel(labelID, userID);
                }
                else
                {
                    throw new Exception("Please enter label ID");
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
        /// <exception cref="Exception">
        /// User not found
        /// or
        /// </exception>
        public IList<LabelResponse> DisplayLabels(string userID)
        {
            try
            {
                if (userID != null)
                {
                    return this.labelRL.DisplayLabels(userID);
                }
                else
                {
                    throw new Exception("User not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
