using Newtonsoft.Json;
using System.Collections.Generic;

namespace RoboschienWeb.Models.Entities.UI
{
    public class GoogleCaptchaResponse
    {
        [JsonProperty("success")]
        public bool IsSuccess { get; set; }
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }
}