namespace CarDealer.Web.Models
{
    public class ErrorModel
    {
        public ErrorModel()
        { }

        public ErrorModel(string message)
        {
            this.Message = message;
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string Message { get; set; }
    }
}