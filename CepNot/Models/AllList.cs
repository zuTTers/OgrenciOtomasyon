using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CepNot.Models
{
    public class AllList
    {  
        public IList<Users> Users { get; set; }
        public IList<Tasks> Tasks { get; set; }
        public IList<Tags> Tags { get; set; }
        public IList<Notes> Notes { get; set; }
        public IList<Roles> Roles { get; set; }

        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        
    }
}