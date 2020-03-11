using CommonLayer.Model;
using CommonLayer.Model.Account;
using CommonLayer.Model.Request.Note;
using CommonLayer.Model.Response;
using CommonLayer.Model.Response.Note;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {

        Task<bool> CreateNote(NoteRequest requestNote, string userID);

        /// <summary>
        /// Displays the notes.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the list of note</returns>
        IList<NoteResponse> DisplayNotes(string userID);

        /// <summary>
        /// Gets the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the note info or null value</returns>
        Task<NoteResponse> GetNote(int noteID, string userID);

        /// <summary>
        /// Updates the note.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the info of label</returns>
        Task<NoteModel> UpdateNote(NoteRequest noteRequest, int noteID, string userID);

        /// <summary>
        /// Deletes the note.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns message indicating operation is done or not</returns>
        Task<bool> DeleteNote(int noteID, string userID);
        /// <summary>
        /// Determines whether the specified note identifier is archieve.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task<bool> IsArchieve(int noteID, string userID);
        /// <summary>
        /// Gets the archieve notes.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        IList<NoteResponse> GetArchieveNotes(NoteRequest noteRequest, string userID);
        /// <summary>
        /// Determines whether the specified note identifier is trash.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task<bool> IsTrash(int noteID, string userID);
        /// <summary>
        /// Restorings the notes.
        /// </summary>
        /// <param name="noteResponse">The note response.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task<bool> RestoreNotes(int noteID,string userID);
        /// <summary>
        /// Determines whether the specified note identifier is pin.
        /// </summary>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task<bool> IsPin(int noteID, string userID);
        /// <summary>
        /// Gets the pin notes.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        IList<NoteResponse> GetPinNotes(string userID);
        /// <summary>
        /// Chaneges the color.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task<NoteModel> ChangeColor(NoteRequest noteRequest, int noteID, string userID);
        /// <summary>
        /// Sets the reminder.
        /// </summary>
        /// <param name="noteRequest">The note request.</param>
        /// <param name="noteID">The note identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task<NoteModel> SetReminder(int noteID, string userID);
        /// <summary>
        /// Removes the reminder.
        /// </summary>
        /// <param name="noteResponse">The note response.</param>
        /// <param name="nodeID">The node identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        Task<NoteModel> RemoveReminder(int nodeID, string userID);
        
    }
}
