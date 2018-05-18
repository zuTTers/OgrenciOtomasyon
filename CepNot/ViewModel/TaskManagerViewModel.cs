using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CepNot.ViewModel
{
    public class TaskManagerDetail
    {
        public int TaskID { get; set; }
        public int AId { get; set; }
        public string AName { get; set; }
        public int SId { get; set; }
        public string SName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int Type { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedUser { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public IList<Models.Tasks> TasksList { get; set; }
    }
}