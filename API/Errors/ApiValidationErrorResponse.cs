namespace API.Errors
{
    // This class extends error response from the ApiResponse that creates consistant error response to client.
    // It obtains an array of error messages that occures when the data sent in forms violates the model validations.
    // An example of such errors would be 400 validation error. 
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(400) {}

        public IEnumerable<string> Errors { get; set; }
    }
}