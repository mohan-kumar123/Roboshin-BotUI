using System;
using System.Collections.Generic;

namespace RoboschienWeb.Models.Entities
{
    public partial class JobExecutionDetail
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public JobConfiguration Job { get; set; }
        public JobConfiguration Status { get; set; }
        public JobStatus StatusNavigation { get; set; }
    }
}
