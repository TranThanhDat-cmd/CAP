using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace CAP_Backend_Source.Common
{
    [Serializable]
    public class HttpResponseException : Exception
    {
        public HttpResponseException()
        {
        }

        public HttpResponseException(string? message)
            : base(message)
        {
        }

        public HttpResponseException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public int StatusCode { get; set; }

        public object? Value { get; set; }

        protected HttpResponseException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }
    }

    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Method intentionally left empty.
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException httpResponseException)
            {
                context.Result = new ObjectResult(httpResponseException.Value)
                {
                    StatusCode = httpResponseException.StatusCode,
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
