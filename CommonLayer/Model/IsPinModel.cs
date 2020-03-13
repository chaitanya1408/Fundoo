using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class IsPinModel
    {
        [Required]
        public bool isPin { get; set; }
    }
}
