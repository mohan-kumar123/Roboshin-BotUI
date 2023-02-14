namespace RoboschienWeb.Models.Entities.UI
{
    public class ApiSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; } = new ConnectionStrings();
        public string OcrMatchText { get; set; }
    }

    public class ConnectionStrings
    {
        public string SickLeaveConnection { get; set; }
        public string StorageConnection { get; set; }
        public string Container { get; set; }
        public string AESKey { get; set; }
        public string OcrKey { get; set; }
        
    }
}
