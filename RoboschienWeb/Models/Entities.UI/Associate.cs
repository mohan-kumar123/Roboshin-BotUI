using Newtonsoft.Json;

namespace RoboschienWeb.Models.Entities.UI
{
    public class Associate
    {
        public string AssociateNumber { get; set; }
        public string AssociateFirstName { get; set; }
        public string AssociateLastName  { get; set; }
        public string AssociateEmail {get; set; }
        public string AssociateMobileNumber { get; set; }
        public string DataPrivacyTimeStamp { get; set; }

        public string SelectedCountryCode { get; set; }
        public bool IsOcrCheck { get; set; }



        [JsonProperty("WorkDisabilityCertificateDetails")]
        public WorkDisabilityCertificateDetails workDisabilityCertificateDetailsInfo { get; set; }
       
    }
}