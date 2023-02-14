

using System.ComponentModel.DataAnnotations;

namespace RoboschienWeb.Models.Entities.UI
{
    public class AppVersionDetails
    {

        public bool IsSuccess { get; set; }
        [Required]
        public string MobileOsName { get; set; }
        public string AppName { get; set; }
        public int VersionCode { get; set; }
        public string VersionName { get; set; }
        public string Message { get; set; }
    }
}
