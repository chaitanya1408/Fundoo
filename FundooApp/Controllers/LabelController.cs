using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.Model.Request;
using CommonLayer.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private ILabelBL labelBL;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="labelBL">The reference of interface of business layer label class.</param>
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="labelRequest">The label request.</param>
        /// <returns>returns the result</returns>
        [HttpPost]
        public async Task<IActionResult> CreateLabel(LabelRequest labelRequest)
        {
            try
            {
                // Find the userID
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                // get the result indicating whether new label is created or not
                var result = await this.labelBL.CreateLabel(labelRequest, userID);
                bool success = false;
                var message = string.Empty;

                // check whether result variable indicates true or false
                if (result)
                {
                    success = true;
                    message = "Label Added successfully";
                    return this.Ok(new { success, message });
                }
                else
                {
                    success = false;
                    message = "Failed";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelRequest">The label request.</param>
        /// <param name="labelID">The label identifier.</param>
        /// <returns>returns the result</returns>
        [HttpPut]
        [Route("{labelID}")]
        public async Task<IActionResult> UpdateLabel(LabelRequest labelRequest, int labelID)
        {
            try
            {
                // Find the userID
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                // gets the updated values for label
                var data = await this.labelBL.UpdateLabel(labelRequest, labelID, userID);
                bool success = false;
                var message = string.Empty;

                // chck whether data variable holds value or not
                if (data != null)
                {
                    success = true;
                    message = "Successfully updated label";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Failed";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <returns>returns the result</returns>
        [HttpDelete]
        [Route("{labelID}")]
        ////Post: /api/Note/DeleteLabel
        public async Task<IActionResult> DeleteLabel(int labelID)
        {
            try
            {
                // Find the userID
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                // check whether label is deleted from tabel or not
                var result = await this.labelBL.DeleteLabel(labelID, userId);
                bool success = false;
                var message = string.Empty;

                // check whether result indicates true or false value
                if (result)
                {
                    success = true;
                    message = "Succeffully Deleted Label";
                    return this.Ok(new { success, message });
                }
                else
                {
                    success = false;
                    message = "LabelID does not exist";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }

        /// <summary>
        /// Displays the labels.
        /// </summary>
        /// <returns>returns the result</returns>
        [HttpGet]
        public async Task<IActionResult> DisplayLabels()
        {
            try
            {
                // gets the user id
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                // get the list of labels
                IList<LabelResponse> data = this.labelBL.DisplayLabels(userId);
                bool success = false;
                var message = string.Empty;

                // check whether data holds list or not
                if (data.Count != 0)
                {
                    success = true;
                    message = "Labels: ";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Labels not available";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }
    }
}