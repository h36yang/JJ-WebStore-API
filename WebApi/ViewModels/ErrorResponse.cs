namespace WebApi.ViewModels
{
    /// <summary>
    /// Standard Error Response Class
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// HTTP Status Code
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// HTTP Status Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="statusCode">HTTP Status Code</param>
        /// <param name="message">HTTP Status Message</param>
        public ErrorResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
