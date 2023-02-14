using Newtonsoft.Json;
using System;

namespace RoboschienWeb.Models.Entities.UI
{
    public class WorkDisabilityCertificateDetails
    {
        [JsonProperty("FirstTimeSickness")]
        public string IsFirstTimeSickness { get; set; }

        [JsonProperty("FollowUpSickness")]
        public string IsFollowupSickness { get; set; }

        [JsonProperty("Accident")]
        public string IsAccident { get; set; }

        [JsonProperty("Hopitalization")]
        public string IsHospitalization { get; set; }
        [JsonProperty("Covid")]
        public string IsCovid { get; set; }
        [JsonProperty("PartTime")]
        public string IsPartTime { get; set; }
        public string IllnessType { get; set; }
        public string KindofIllness { get; set; }
        public string RequestType { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string FollowupDate { get; set; }
        public string AgreeContactDataTimeStamp { get; set; }
        //public string FollowupDate { get; set; }
    }
}