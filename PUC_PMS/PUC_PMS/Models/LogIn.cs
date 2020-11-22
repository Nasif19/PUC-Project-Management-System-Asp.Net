using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUC_PMS.Models
{
    public class LogIn
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Roll { get; set; }
        public string Type { get; set; }
        public int Salt { get; set; }
        

    }
}