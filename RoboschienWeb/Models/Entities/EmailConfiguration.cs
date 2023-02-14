using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboschienWeb.Models.Entities
{
    public partial class EmailConfiguration
    {
        public EmailConfiguration()
        {
            
        }

        public int id { get; set; }
        public string EmailType { get; set; }
        public string EmailSubject { get; set; }
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string MailContentPart1 { get; set; }
        public string  MailContentPart2 { get; set; }
        public string MailContentPart3 { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
