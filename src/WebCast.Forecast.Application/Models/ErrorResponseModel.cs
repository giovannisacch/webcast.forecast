namespace WebCast.Forecast.Application.Models
{
    public class ErrorResponseModel
    {
        public string Message { get; set; }
        public ErrorResponseModel()
        {

        }
        public ErrorResponseModel(string message)
        {
            Message = message;
        }
    }
}
