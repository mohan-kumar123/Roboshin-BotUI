using System.Collections.Generic;

namespace RoboschienWeb.Models.Entities.UI
{
    public class ResponseDetails
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public List<ResponseData> ResponseDataDetails { get; set; }
    }
}