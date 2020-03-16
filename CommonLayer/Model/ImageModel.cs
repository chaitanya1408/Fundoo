using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class ImageModel
    {
        [Required]
        public IFormFile iformFile { get; set; }
    }
}
