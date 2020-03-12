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
        [Route("CreateNote")]
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
        [Route("DeleteNote")]
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
        [Route("UpdateNote")]
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
        [Route("DisplayNote")]
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
        [Route("GetNote")]
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
        [HttpPut]
        [Route("IsArchieve")]
        public async Task<IActionResult>IsArchieve(bool IsArchieve,int noteID)
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.IsArchieve(IsArchieve,noteID, userID);
                bool success = false;
                var message = string.Empty;

                if (data!=null)
                {
                    success = true;
                    message = "Note is updated";
                    return this.Ok(new { success, message,data });
                }
                else
                {
                    success = false;
                    message = "Note is not updated";
                    return this.BadRequest(new { success, message });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet]
        [Route("GetArchieveNotes")]
        public async Task<IActionResult>GetArchieveNotes()
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                IList<NoteResponse> data = this.noteBL.GetArchieveNotes(userID);

                bool success = false;
                var message = string.Empty;
                if (data.Count != 0)
                {
                    success = true;
                    message = "All Archievenotes Are displayed";
                    return this.Ok(new {data, success, message });
                }
                else
                {
                    success = false;
                    message = "Note is not found";
                    return this.BadRequest(new { success, message });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut]
        [Route("IsTrash")]
        public async Task<IActionResult>IsTrash(bool IsTrash,int noteID)
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.IsTrash(IsTrash,noteID, userID);
                bool success = false;
                var message = string.Empty;
                if (data!=null)
                {
                    success = true;
                    message = "Note is Updated";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Note is not updated";
                    return this.BadRequest(new { success, message });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut]
        [Route("RestoringNotes")]
        public async Task<IActionResult>RestoreNotes(int noteID)
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.RestoreNotes(noteID, userID);
                bool success = false;
                var message = string.Empty;
                if (data!=null)
                {
                    success = true;
                    message = "Notes Are restored";
                    return this.Ok(new { success, message ,data});
                }
                else
                {
                    success = false;
                    message = "Note Restoration Failed";
                    return this.BadRequest(new { success, message });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut]
        [Route("BulkRestore")]
        public async Task<IActionResult> BulkRestore()
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.BulkRestore(userID);
                bool success = false;
                var message = string.Empty;
                if (data != null)
                {
                    success = true;
                    message = "Restored all Notes";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "not restored notes";
                    return this.BadRequest(new { success, message, data });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut]
        [Route("IsPin")]
        public async Task<IActionResult>IsPin(int noteID)
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.IsPin(noteID, userID);
                bool success = false;
                var message = string.Empty;
                if (data!=null)
                {
                    success = true;
                    message = "note is Updated";
                    return this.Ok(new { success, message });
                }
                else
                {
                    success = false;
                    message = "Note is not updated";
                    return this.BadRequest(new { success, message });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet]
        [Route("AllPinnedNotes")]
        public async Task<IActionResult> GetPinNotes()
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
               IList<NoteResponse> data = this.noteBL.GetPinNotes(userID);
                var succees = false;
                var message = string.Empty;
                if (data.Count!=0)
                {
                    succees = true;
                    message="PinnedNOtes";
                    return this.Ok(new { succees, message, data });
                }
                else
                {
                    succees = false;
                    message = "Note is not updated";
                    return this.BadRequest(new { succees, message });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        

        [HttpPut]
        [Route("ChangeColour")]
        public async Task<IActionResult>ChangeColor(string color, int noteID)
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.ChangeColor(color, noteID, userID);
                bool success = false;
                var message = string.Empty;
                if (data != null)
                {
                    success = true;
                    message = "Colour is changed";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Note is not Updated";
                    return this.BadRequest(new { success, message });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut]
        [Route("SetReminder")]
        public async Task<IActionResult>SetReminder(DateTime reminder,int noteID)
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.SetReminder(reminder,noteID, userID);
                bool success = false;
                var message = string.Empty;
                if (data != null)
                {
                    success = true;
                    message = "Reminder Set";
                    return this.Ok(new { success, message, data });
                }
                else
                {
                    success = false;
                    message = "Reminder is not set";
                    return this.BadRequest(new { success, message });
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpPut]
        [Route("RemoveReminder")]
        public async Task<IActionResult>RemoveReminder(int noteID)
        {
            try
            {
                var userID = HttpContext.User.Claims.First(c => c.Type == "UserID").Value;
                var data = await this.noteBL.RemoveReminder(noteID, userID);
                bool success = false;
                var message = string.Empty;
                if (data != null)
                {
                    success = true;
                    message = "Reminder Removed";
                    return this.Ok(new { success, message });
                }
                else
                {
                    success = false;
                    message = "Reminder is Not Removied";
                    return this.BadRequest(new { success, message });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}