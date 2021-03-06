﻿using BusinessLayer.Interface;
using CommonLayer.Model;
using CommonLayer.Model.Request.Note;
using CommonLayer.Model.Response;
using CommonLayer.Model.Response.Note;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        private readonly INoteRL noteRL;

        
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

       
        public async Task<NoteResponse> CreateNote(NoteRequest noteRequest, string userID)
        {
            try
            {
                // check whether user entered null data or not
                if (noteRequest != null)
                {
                    // if user entered corrrect data then pass user entered data and user id to repository layer method to create note
                    return await this.noteRL.CreateNote(noteRequest, userID);
                }
                else
                {
                    // if user enter any null data then throw exception
                    throw new Exception("Data Required");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns message indicating operation is done or not
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter Note ID
        /// or
        /// </exception>
        public async Task<bool> DeleteNote(int noteID, string userID)
        {
            try
            {
                // check whther user enter currect note id or not
                if (noteID > 0)
                {
                    // if user enter correct note id then pass that note id and user id to repository layer method to delete note
                    var result = await this.noteRL.DeleteNote(noteID, userID);
                    return result;
                }
                else
                {
                    // if user enter wrong note id then throw exception
                    throw new Exception("Please enter correct Note ID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

       
        public async Task<NoteModel> UpdateNote(NoteRequest noteRequest, int noteID, string userID)
        {
            try
            {
                // check whther user enter currect note id or not
                if (noteID > 0)
                {
                    // if user enter correct note id then pass that note id, user entered data and user id to repository layer method to update note info
                    return await this.noteRL.UpdateNote(noteRequest, noteID, userID);
                }
                else
                {
                    // if user enter wrong note id then throw exception
                    throw new Exception("Please enter correct Note ID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

       
        public IList<NoteResponse> DisplayNotes(string userID)
        {
            try
            {
                // check whther user id is null or not
                if (userID != null)
                {
                    // if user id is not null then pass that id to repository layer method to display notes of that user
                    return this.noteRL.DisplayNotes(userID);
                }
                else
                {
                    // if user id contains null value then throw exception
                    throw new Exception("User not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

       
        public async Task<NoteResponse> GetNote(int noteID, string userID)
        {
            try
            {
                // check whther user id is null or not
                if (userID != null)
                {
                    // if user id is not null then pass that id to repository layer method to display notes of that user
                    return await this.noteRL.GetNote(noteID, userID);
                }
                else
                {
                    // if user id contains null value then throw exception
                    throw new Exception("User not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public async Task<NoteResponse>IsArchieve(IsArchieveModel isArchieve,int noteID,string userID)
        {
            try
            {
                //check whether the userId is not null
                if (userID != null)
                {
                    //if usrerid is not null then pass that id to Respository layer method to display notes of that user
                    return await this.noteRL.IsArchieve(isArchieve,noteID, userID);
                }
                else
                {
                    //if user id contains null value then throw exception
                    throw new Exception("User Not found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IList<NoteResponse>GetArchieveNotes(string userID)
        {
            try
            {

                if (userID != null)
                {
                    return this.noteRL.GetArchieveNotes(userID);
                }
                else
                {
                    throw new Exception("Data not Found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<NoteResponse> IsTrash(IsTrashModel isTrash,int noteID, string userID)
        {
            try
            {
                if (userID != null)
                {
                    return await this.noteRL.IsTrash(isTrash,noteID, userID);
                }
                else
                {
                    throw new Exception("User Not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<NoteResponse>RestoreNotes(int noteID,string userID)
        {
            try
            {
                if (userID != null)
                {
                    return await this.noteRL.RestoreNotes(noteID, userID);
                }
                else
                {
                    throw new Exception("User Not Found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<NoteResponse>BulkRestore(string userID)
        {
            try
            {
                if (userID != null)
                {
                    return await this.noteRL.BulkRestore(userID);
                }
                else
                {
                    throw new Exception("User not Found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<NoteResponse>IsPin(IsPinModel ispin,int noteID,string userID)
        {
            try
            {
                if (userID != null)
                {
                    return await this.noteRL.IsPin(ispin,noteID, userID);
                }
                else
                {
                    throw new Exception("User not found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IList<NoteResponse>GetPinNotes(string userID)
        {
            try
            {
                if (userID != null)
                {
                    return this.noteRL.GetPinNotes(userID);
                }
                else
                {
                    throw new Exception("User NOt found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<NoteResponse>ChangeColor(ColorModel color,int noteID,String userID)
        {
            try
            {
                if (userID != null)
                {
                    return await this.noteRL.ChangeColor(color,noteID, userID);
                }
                else
                {
                    throw new Exception("User not found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<NoteResponse>SetReminder(ReminderModel reminder,int noteID,string userID)
        {
            try
            {
                if (userID != null)
                {
                    return await this.noteRL.SetReminder( reminder,noteID, userID);
                }
                else
                {
                    throw new Exception("User Not Found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<NoteResponse>RemoveReminder(int noteID,string userID)
        {
            try
            {
                if (userID != null)
                {
                    return await this.noteRL.RemoveReminder(noteID, userID);
                }
                else
                {
                    throw new Exception("User not Found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<NoteResponse>UploadImage(int noteID,string userID,IFormFile file)
        {
            try
            {
                if (userID != null)
                {
                    return await this.noteRL.UploadImage(noteID, userID,file); 
                }
                else
                {
                    throw new Exception("user not found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //public async Task<NoteResponse> RemoveImage(ImageModel image, int noteID, string userID)
        //{
        //    try
        //    {
        //        if (userID != null)
        //        {
                    
        //        }
        //    }
        //}
    } 
}
