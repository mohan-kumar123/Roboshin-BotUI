using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboschienWeb.Models.Entities.UI
{
    public class SessionResponseDetails
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public List<SessionDetails> ResponseDataDetails { get; set; }
    }
}
