﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class ColorModel
    {
        [Required]
        public string color { get; set; }
    }
}
