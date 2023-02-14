using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboschienWeb.Models.Entities
{
    public class GlobalReferenceNumberDetails
    {
        public int Id { get; set; }

        public long ReferenceNumber { get; set; }
        public string CountryCode { get; set; }
    }
}
