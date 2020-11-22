using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRunningBlog.Models
{
    public class RunningEntry
    {
        public int RunningId { get; set; }
        public DateTime Date { get; set; }
        public double RunningPace { get; set; }
        public double RunningTime { get; set; }
        public double RunningDistance { get; set; }
        public string RunningComments { get; set; }
    }
}
