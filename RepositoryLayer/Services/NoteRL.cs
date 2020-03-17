using CommonLayer.Model;

using CommonLayer.Model.Request.Note;
using CommonLayer.Model.Response;
using CommonLayer.Model.Response.Note;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.ImageUpload;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        /// </summary>
        private readonly ApplicationSetting applicationSetting;

        /// <summary>
        /// creating reference of authentication context class
        /// </summary>
        private AuthenticationContext authenticationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoteRL"/> class.
        /// </summary>
        /// <param name="authenticationContext">The authentication context.</param>
        /// <param name="applSetting">The application setting.</param>
        public NoteRL(AuthenticationContext authenticationContext, IOptions<ApplicationSetting> applSetting)
        {
            this.applicationSetting = applSetting.Value;
            this.authenticationContext = authenticationContext;
        }

        /// <summary>
        /// Creates the note.
        /// </summary>
        /// <param name="requestNote">The request note.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns true or false indicating operation is successful or not</returns>
        /// <exception cref="Exception"> exception message</exception>
        public async Task<NoteResponse> CreateNote(NoteRequest requestNote,string userID)
        {
            try
            {
                // check whether user enter all the required data or not
                if (requestNote != null)
                {
                    // set the user entered values
                    var data = new NoteRequest()
                    {
                        Title = requestNote.Title,
                        Description = requestNote.Description,
                        Reminder = requestNote.Reminder,
                        Collaborator = requestNote.Collaborator,
                        Color = requestNote.Color,
                        Image = requestNote.Image,
                        IsArchive = requestNote.IsArchive,
                        IsPin = requestNote.IsPin,
                        IsTrash = requestNote.IsTrash,
                    };
                    if (data.Collaborator != null)
                    {
                        var noteInfo = new NoteModel()
                        {
                            UserID = userID,
                            Title = data.Title,
                            Description = data.Description,
                            Collaborators = data.Collaborator,
                            Color = data.Color,
                            Reminder = data.Reminder,
                            Image = data.Image,
                            IsArchive = data.IsArchive,
                            IsPin = data.IsPin,
                            IsTrash = data.IsTrash,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        };


                        // add new note in tabel
                        this.authenticationContext.Note.Add(noteInfo);

                        // save the changes in database
                        await this.authenticationContext.SaveChangesAsync();

                        var user = this.authenticationContext.UserDataTable.Where(s => s.Email == noteInfo.UserID);

                        var colaborator = new ColaboratorModel()
                        {
                            UserID = userID,
                            NoteID = noteInfo.NoteID,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        };
                        this.authenticationContext.Colaborator.Add(colaborator) ;
                        await this.authenticationContext.SaveChangesAsync();

                        NoteResponse nr = this.GetNoteResponse(userID, noteInfo);
                        return nr;
                    }
                    else
                    {
                        var noteInfo = new NoteModel()
                        {
                            UserID = userID,
                            Title = data.Title,
                            Description = data.Description,
                            Collaborators = data.Collaborator,
                            Color = data.Color,
                            Reminder = data.Reminder,
                            Image = data.Image,
                            IsArchive = data.IsArchive,
                            IsPin = data.IsPin,
                            IsTrash = data.IsTrash,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now
                        };
                        this.authenticationContext.Note.Add(noteInfo);
                        await this.authenticationContext.SaveChangesAsync();

                        NoteResponse nr = this.GetNoteResponse(userID, noteInfo);
                        return nr;
                    }

                }
                else
                {
                    throw new Exception("Note is not created");
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
        /// <exception cref="Exception"> exception message</exception>
        public async Task<bool> DeleteNote(int noteID, string userID)
        {
            try
            {
                // get required note for user
                var note = this.authenticationContext.Note.Where(s => s.NoteID == noteID && s.UserID == userID).FirstOrDefault();

                // check whether user have required note or not
                if (note != null)
                {
                    // delete the note from note tabel
                    this.authenticationContext.Note.Remove(note);
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
        /// Updates the note.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the info of label
        /// </returns>
        /// <exception cref="Exception"> exception message</exception>
        public async Task<NoteModel> UpdateNote(NoteRequest noteRequest, int noteID, string userID)
        {
            try
            {
                // get required note for user
                var note = this.authenticationContext.Note.Where(s => s.NoteID == noteID && s.UserID == userID).FirstOrDefault();

                // check whether user have required note or not
                if (note != null)
                {
                    // set the current date and time 
                    note.ModifiedDate = DateTime.Now;

                    // check whether user entered value for note title or not
                    if (noteRequest.Title != null && noteRequest.Title != string.Empty)
                    {
                        note.Title = noteRequest.Title;
                    }

                    // check whether user entered value for note Description or not
                    if (noteRequest.Description != null && noteRequest.Description != string.Empty)
                    {
                        note.Description = noteRequest.Description;
                    }
                    //check whether colaboraterequest is gereater hit or not
                    if (noteRequest.Collaborator > 0)
                    {
                        note.Collaborators = noteRequest.Collaborator;
                    }
                    // check whether user entered value for color or not
                    if (noteRequest.Color != null && noteRequest.Color != string.Empty)
                    {
                        note.Color = noteRequest.Color;
                    }

                    // check whether user entered value for image or not
                    if (noteRequest.Image != null && noteRequest.Image != string.Empty)
                    {
                        note.Image = noteRequest.Image;
                    }

                    // check whether user entered value for Archive or not
                    if (!noteRequest.IsArchive.Equals(note.IsArchive))
                    {
                        note.IsArchive = noteRequest.IsArchive;
                    }

                    // check whether user entered value for Pin or not
                    if (!noteRequest.IsPin.Equals(note.IsPin))
                    {
                        note.IsPin = noteRequest.IsPin;
                    }

                    // check whether user entered value for Trash or not
                    if (!noteRequest.IsTrash.Equals(note.IsTrash))
                    {
                        note.IsTrash = noteRequest.IsTrash;
                    }

                    // check whether user entered value for Reminder or not
                    if (noteRequest.Reminder >= DateTime.Now)
                    {
                        note.Reminder = noteRequest.Reminder;
                    }

                    // update the changes
                    this.authenticationContext.Note.Update(note);
                    await this.authenticationContext.SaveChangesAsync();
                    return note;
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
        /// Displays the notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the list of note
        /// </returns>
        /// <exception cref="Exception"> exception message</exception>
        public IList<NoteResponse> DisplayNotes(string userID)
        {
            try
            {
                // get all notes of user
                var data = this.authenticationContext.Note.Where(s => s.UserID == userID) ;
                var list = new List<NoteResponse>();

                // check whether user have notes or not
                if (data != null)
                {
                    // iterates the loop for each note
                    foreach (var note in data)
                    {
                        NoteResponse notes = this.GetNoteResponse(userID, note);

                        list.Add(notes);
                    }
                    // returns the list
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

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the operation result
        /// </returns>
        /// <exception cref="Exception"> exception message</exception>
        public async Task<NoteResponse> GetNote(int noteID, string userID)
        {
            try
            {
                // get all notes of user
                var data = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();

                // check whether user have notes or not
                if (data != null)
                {
                    NoteResponse note = this.GetNoteResponse(userID, data);
                    return note;
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

        public async Task<NoteResponse>IsArchieve(IsArchieveModel isArchieve,int noteID,string userID)
        {
            try
            {
                
                //get all the notes
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();
                //check whether the data is null ornot 
                if (note != null)
                {
                    note.IsArchive = isArchieve.IsArchieve;
                    note.ModifiedDate = DateTime.Now;
                    this.authenticationContext.Note.Update(note);
                    await this.authenticationContext.SaveChangesAsync();
                    NoteResponse data = this.GetNoteResponse(userID, note);
                    return data;
                }
                //data not found
                else
                {
                    throw new Exception("data not found");
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
                //get all notes
                var data = this.authenticationContext.Note.Where(s => s.UserID == userID && s.IsArchive == true);
                var list = new List<NoteResponse>();
                //check whether data is null or not
                if (data != null)
                {
                    foreach (var note in data)
                    {
                        NoteResponse notes = this.GetNoteResponse(userID, note);

                        list.Add(notes);

                    }
                    return list;
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
        public async Task<NoteResponse> IsTrash(IsTrashModel isTrash,int noteID, string userID)
        {
            try
            {
                //get all notes
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();
                //check whether data is null or not
                if (note != null)
                {
                    note.IsTrash = isTrash.IsTrash;
                    note.ModifiedDate = DateTime.Now;
                    this.authenticationContext.Note.Update(note);
                    await this.authenticationContext.SaveChangesAsync();
                    NoteResponse data = this.GetNoteResponse(userID, note);
                    return data;
                }
                else
                {
                    throw new Exception("Data Not found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public async Task<NoteResponse>RestoreNotes(int noteID,string userID)
        {
            try
            {
                //get the all the notes
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID&&s.NoteID==noteID && s.IsTrash == true).FirstOrDefault();
               //checking data is null or not
                if (note != null)
                {
                    note.IsTrash = false;
                    note.ModifiedDate = DateTime.Now;
                    this.authenticationContext.Note.Update(note);
                    NoteResponse data = this.GetNoteResponse(userID, note);
                    return data;
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
        public async Task<NoteResponse>BulkRestore(string userID)
        {
            try
            {
                var note = this.authenticationContext.Note.Where(s => s.UserID == s.UserID && s.IsTrash == s.IsTrash).FirstOrDefault();
                if (note != null)
                {
                    note.IsTrash = false;
                    note.ModifiedDate = DateTime.Now;
                    this.authenticationContext.Note.Update(note);
                    NoteResponse data = this.GetNoteResponse(userID, note);
                    return data;
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
        public async Task<NoteResponse>IsPin(IsPinModel isPin,int noteID,string userID)
        {
            try
            {
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();
                //check whether data is null or not
                if (note != null)
                {
                    note.IsTrash = isPin.isPin;
                    note.ModifiedDate = DateTime.Now;
                    this.authenticationContext.Note.Update(note);
                    await this.authenticationContext.SaveChangesAsync();
                    NoteResponse data = this.GetNoteResponse(userID, note);
                    return data;
                }
                else
                {
                    throw new Exception("Matching Data Not found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public IList<NoteResponse> GetPinNotes(string userID)
        {
            try
            {
                //get all notes
                var data = this.authenticationContext.Note.Where(s => s.UserID == userID && s.IsPin == true);
                //initialize list
                var list = new List<NoteResponse>();
                //checking whether data is null or not     
                if (data != null)
                {
                    foreach (var note in data)
                    {
                        NoteResponse notes = this.GetNoteResponse(userID, note);

                        list.Add(notes);

                    }
                    return list;
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
        public async Task<NoteResponse>ChangeColor(ColorModel color, int noteID,string userID)
        {
            try
            {
                //geting note
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();
                //checking whether note is null or not
                if (note != null)
                {
                    //color is null or not
                    note.Color = color.color;
                    note.ModifiedDate = DateTime.Now;
                    this.authenticationContext.Note.Update(note);
                    await this.authenticationContext.SaveChangesAsync();
                    NoteResponse data = this.GetNoteResponse(userID, note);
                    return data;
                }
                else
                {
                    throw new Exception("Note is not Found");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public async Task<NoteResponse> SetReminder(ReminderModel reminder,int noteID, string userID)
        {
            try
            {
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();
                if (note != null)
                {
                    note.Reminder = reminder.Reminder;
                    note.ModifiedDate = DateTime.Now;
                    this.authenticationContext.Note.Update(note);
                    await this.authenticationContext.SaveChangesAsync();
                    NoteResponse data = this.GetNoteResponse(userID, note);
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<NoteResponse> UploadImage(int noteID, string userID,IFormFile file)
        {
            try
            {
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();

                if (note != null && note.IsTrash == false)
                {
                    UploadImage imageUpload = new UploadImage(this.applicationSetting.APIkey, this.applicationSetting.APISecret, this.applicationSetting.CloudName);

                    var url = imageUpload.Upload(file);

                    note.Image = url;
                    note.ModifiedDate = DateTime.Now;

                    this.authenticationContext.Note.Update(note);

                    await this.authenticationContext.SaveChangesAsync();

                    NoteResponse data = this.GetNoteResponse(userID, note);

                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<NoteResponse>RemoveReminder(int noteID,string userID)
        {
            try
            {
                var note = this.authenticationContext.Note.Where(s => s.UserID == userID && s.NoteID == noteID).FirstOrDefault();
                if (note != null)
                {
                    note.Reminder =null ;
                    note.ModifiedDate = DateTime.Now;
                    this.authenticationContext.Note.Update(note);
                    await this.authenticationContext.SaveChangesAsync();
                    NoteResponse data = this.GetNoteResponse(userID, note);
                    return data;
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
        

        private NoteResponse GetNoteResponse(string userID, NoteModel note)
        {
            try
            {
                // get labels for Note
                var labelList = this.authenticationContext.NoteLabel.Where(s => s.UserID == userID && s.NoteID == note.NoteID);

                // creating list for label
                var labels = new List<LabelResponse>();

                // iteartes the loop for each label for note
                foreach (var data in labelList)
                {
                    // get the label info
                    var label = new LabelResponse()
                    {
                        ID = data.LabelID,
                        Label = data.Label,
                    };

                    // add the label info into label list
                    labels.Add(label);
                }

                // get the required values of note
                var notes = new NoteResponse()
                {
                    NoteID = note.NoteID,
                    Title = note.Title,
                    Description = note.Description,
                    Color = note.Color,
                    Image = note.Image,
                    IsArchive = note.IsArchive,
                    IsPin = note.IsPin,
                    IsTrash = note.IsTrash,
                    Reminder = note.Reminder,
                    Labels = labels
                };
                // return the note info
                return notes;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
