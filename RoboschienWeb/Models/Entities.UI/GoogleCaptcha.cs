using Newtonsoft.Json;

namespace RoboschienWeb.Models.Entities.UI
{
    public class GoogleCaptcha
    {
        [JsonProperty("secret")]
        public string secret { get; set; }
        [JsonProperty("response")]
        public string response { get; set; }
       

    }
}