using Newtonsoft.Json;

namespace RoboschienWeb.Models.Entities.UI
{
    public class Captcha
    {
        [JsonProperty("requestSource")]
        public string RequestSource { get; set; }

        [JsonProperty("response")]
        public string UserResponse { get; set; }
      
    }
}