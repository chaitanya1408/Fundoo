using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.Model.Account;
using CommonLayer.Model.Request.Note;
using CommonLayer.Model.Response;
using CommonLayer.Model.Response.Note;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;

        
        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }

      
        [HttpPost]
        public async Task<IActionResult> CreateNote(NoteRequest noteRequest)
        {
            try
            {
                
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                var data = await this.noteBL.CreateNote(noteRequest, userId);
                bool success = false;
                var message = string.Empty;

                // check whether result is true or false
                if (data!=null)
                {
                    // if true then return the result 
                    success = true;
                    message = "Succeffully Created Note";
                    return this.Ok(new { success, message,data });
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

       
        [HttpDelete]
        [Route("{noteID}")]
        ////Post: /api/Note/DeleteNote
        public async Task<IActionResult> DeleteNote(int noteID)
        {
            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var result = await this.noteBL.DeleteNote(noteID, userId);

                bool success = false;
                var message = string.Empty;

                // check whether result is true or false
                if (result)
                {
                    success = true;
                    message = "Succeffully Deleted Note";
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

       
        [HttpPut]
        [Route("{noteID}")]
        ////Put: /api/Note/UpdateNote
        public async Task<IActionResult> UpdateNote(NoteRequest noteRequest, int noteID)
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                var result = await this.noteBL.UpdateNote(noteRequest, noteID, userID);

                bool success = false;
                var message = string.Empty;
                if (result != null)
                {
                    success = true;
                    message = "Updated Successfully";
                    return this.Ok(new { success, message, result });
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
                return this.BadRequest(exception.Message);
            }
        }

       
        [HttpGet]
        public async Task<IActionResult> DisplayNotes()
        {
            try
            {
                
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;

                IList<NoteResponse> data = this.noteBL.DisplayNotes(userId);

                bool success = false;
                var message = string.Empty;

                if (data.Count != 0)
                {
                    success = true;
                    return this.Ok(new { success, data });
                }
                else
                {
                    success = false;
                    message = "Notes doesn't exist";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception exception)
            {
                return this.BadRequest(new { exception.Message });
            }
        }

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <returns> returns the info of specific note</returns>
        [HttpGet]
        [Route("{noteID}")]
        ////Post: /api/Note/DisplayNotes
        public async Task<IActionResult> GetNote(int noteID)
        {
            try
            {
                var userId = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.GetNote(noteID, userId);
                bool success = false;
                var message = string.Empty;

                if (data != null)
                {
                    success = true;
                    message = "Note Found ";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Note not Found";
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