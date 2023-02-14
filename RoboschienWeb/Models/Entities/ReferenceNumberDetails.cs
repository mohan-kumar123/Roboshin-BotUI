

using System;

namespace RoboschienWeb.Models.Entities
{
    public class ReferenceNumberDetails
    {
        public int Id { get; set; }

        public long ReferenceNumber { get; set; }
        public string CountryCode { get; set; }

        public static implicit operator ReferenceNumberDetails(int v)
        {
            throw new NotImplementedException();
        }
    }
}
