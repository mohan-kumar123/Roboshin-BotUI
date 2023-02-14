using System;
using System.Collections.Generic;

namespace RoboschienWeb.Models.Entities
{
    public partial class JobStatus
    {
        public JobStatus()
        {
            JobExecutionDetails = new HashSet<JobExecutionDetail>();
        }

        public int Id { get; set; }
        public string JobStatus1 { get; set; }
        public string StatusDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<JobExecutionDetail> JobExecutionDetails { get; set; }
    }
}
