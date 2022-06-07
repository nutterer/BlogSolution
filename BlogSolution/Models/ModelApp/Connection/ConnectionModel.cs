using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSolution.Models.ModelApp.Connection
{
    public class ConnectionModel
    {
        public string ProfileID { get; set; }
        public string ProfileName { get; set; }
        public string Host { get; set; }
        public string DataBase { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IndexProfile { get; set; }
        public bool Selected { get; set; }
    }
}