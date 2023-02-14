using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboschienWeb.Models.Entities
{
    public class RequestTypeDetails
    {
        public int Id { get; set; }
        public string RequestType { get; set; }

        public string CountryCode { get; set; }
    }
}
