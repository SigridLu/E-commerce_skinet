namespace API.Errors
{
    // This class extends error response from the ApiResponse that creates consistant error response to client.
    // It obtains the detail error logs, we'll be using middleware to handle such error. 
    // An example of such errors would be 500 internal server error. 
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null, string details = null)
        : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}