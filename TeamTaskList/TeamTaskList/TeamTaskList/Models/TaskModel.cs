using System;
using System.Collections.Generic;
using System.Text;

namespace TeamTaskList.Models
{
    public class TaskModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        //public DateTime Deadline { get; set; }
        //public List<Person> assigned { get; set; }
    }
}
