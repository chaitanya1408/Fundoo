using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model.Request
{
    public class LabelRequest
    {
        [Required]
        public string Label { get; set; }
    }
}
