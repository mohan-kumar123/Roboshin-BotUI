using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboschienWeb.Models.Entities.UI
{
    public class RequestTypeRespone
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }

        public List<RequestTypeDetails> RequestTypes { get; set; }
        public List<KindofIllnessDetails> IllnessTypesSP { get; set; }
    }
}
