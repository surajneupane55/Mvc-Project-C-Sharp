using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MvcApp.Models
{
    
        public class DetailView
        {



            public Admin Admin { get; set; }
            public Employee Employee { get; set; }
            public Customer Customer { get; set; }
            public User User { get; set; }


        
    }
}