using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace RoboschienWeb.Models.Entities.UI
{
    public class CustomCaptcha
    {
        public Guid CaptchaId { get; set; }
        public string CaptchaText { get; set; }
        public string CaptchaImage { get; set; }
        public DateTime CretedOn { get; set; }
    }
}
