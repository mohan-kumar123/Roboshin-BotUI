using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboschienWeb.Models.Entities
{
    public class EmployeeLeaveInformation_PT
    {

        public EmployeeLeaveInformation_PT()
        {
            EmailInformations = new HashSet<EmailInformation>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string BlobId { get; set; }
        public string EmployeeId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string EmailId { get; set; }
        public string IllnessType { get; set; }
        public string IsFirstTimeIllness { get; set; }
        public string IsFollowupSickness { get; set; }
        public string Locale { get; set; }
        public string ReferenceNumber { get; set; }
        public string RequestSource { get; set; }
        public string DataPrivacyTimeStamp { get; set; }
        public string SessionKey { get; set; }
        public string SecretKey { get; set; }
        public bool? IsSynced { get; set; }
        public string CertificateData { get; set; }
        public bool? Consent { get; set; }
        public string SelectedCountryCode { get; set; }
        public bool IsOcrCheck { get; set; }

        public ICollection<EmailInformation> EmailInformations { get; set; }
    }
}
