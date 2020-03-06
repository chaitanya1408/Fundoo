using CommonLayer.Model;
using CommonLayer.Model.Request;
using CommonLayer.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        Task<bool> CreateLabel(LabelRequest requestLabel, string userID);

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelRequest">The label request.</param>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns the info of label</returns>
        Task<LabelResponse> UpdateLabel(LabelRequest labelRequest, int labelID, string userID);

        /// <summary>
        /// Displays the labels.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns the list of label</returns>
        IList<LabelResponse> DisplayLabels(string userID);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns message indicating operation is done or not</returns>
        Task<bool> DeleteLabel(int labelID, string userID);
    }
}
