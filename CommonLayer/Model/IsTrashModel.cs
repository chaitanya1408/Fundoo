using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class IsTrashModel
    {
        [Required]
        public bool IsTrash { get; set; }
    }
}
