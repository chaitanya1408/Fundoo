using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class ColaboratorModel
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("RegistrationModel")]
        public string UserID { get; set; }

       [ForeignKey("NoteModel")]
        public int NoteID { get; set; }
       
        [Column(TypeName ="DateTime")]
        public DateTime CreatedDate { get; set; }
        
        [Column(TypeName ="DateTime")]
        public DateTime ModifiedDate { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}
