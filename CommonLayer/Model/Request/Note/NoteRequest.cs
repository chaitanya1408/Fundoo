using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model.Request.Note
{
    public class NoteRequest
    {
        public string Title { get; set; }

        
        public string Description { get; set; }

        
        public DateTime? Reminder { get; set; }

       
        public int Collaborator { get; set; }

        
        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")]
        public string Color { get; set; }

       
        public bool IsArchive { get; set; }

        
        public bool IsPin { get; set; }

        public bool IsTrash { get; set; }

        
        public string Image { get; set; }
    }
}
