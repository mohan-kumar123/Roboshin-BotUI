using System;
using System.Collections.Generic;

namespace RoboschienWeb.Models.Entities
{
    public partial class ErrorLog
    {
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string MethodName { get; set; }
        public string ExceptionDetails { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
