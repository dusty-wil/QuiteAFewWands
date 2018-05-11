using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuiteAFewWands
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccId { get; set; }
        public int IsAdmin { get; set; }
    }
}