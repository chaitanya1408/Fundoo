using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class ApplicationSetting
    {
        public string JWTSecret { get; set; }

        public string ClientURL { get; set; }

        public string CloudName { get; set; }

        public string APIkey { get; set; }

        public string APISecret { get; set; }
    }
}
