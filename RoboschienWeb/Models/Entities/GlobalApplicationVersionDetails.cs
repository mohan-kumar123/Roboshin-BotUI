

using System;

namespace RoboschienWeb.Models.Entities
{
    public class GlobalApplicationVersionDetails
    {
        public int ID { get; set; }
        public string AppName { get; set; }
        public int VersionCode { get; set; }
        public string VersionName { get; set; }
        public string MobileOsName { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }



    }
}
