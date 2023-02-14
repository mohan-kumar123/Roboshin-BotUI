using System;
using System.Collections.Generic;

namespace RoboschienWeb.Models.Entities
{
    public partial class EmailInformation
    {
        public int Id { get; set; }
        public int LeaveId { get; set; }
        public string EmailContent { get; set; }
        public string BccMailId { get; set; }
        public bool? IsEmailSent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public EmployeeLeaveInformation Leave { get; set; }
    }
}
