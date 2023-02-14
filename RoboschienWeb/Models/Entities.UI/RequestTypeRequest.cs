using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RoboschienWeb.Models.Entities.UI
{
    public class RequestTypeRequest
    {
        [Required]
        public string CountryCode { get; set; }

    }
}
