using System;
using System.Collections.Generic;

namespace RoboschienWeb.Models.Entities
{
    public partial class JobConfiguration
    {
        public JobConfiguration()
        {
            JobExecutionDetailJobs = new HashSet<JobExecutionDetail>();
            JobExecutionDetailStatus = new HashSet<JobExecutionDetail>();
        }

        public int Id { get; set; }
        public string JobName { get; set; }
        public string JobFrequency { get; set; }
        public string JobDescription { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<JobExecutionDetail> JobExecutionDetailJobs { get; set; }
        public ICollection<JobExecutionDetail> JobExecutionDetailStatus { get; set; }
    }
}
