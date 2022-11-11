using CAP_Backend_Source.Common;
using System.Runtime.Serialization;

namespace Infrastructure.Exceptions.HttpExceptions
{
    [Serializable]
    public sealed class BadRequestException : HttpResponseException
    {
        public BadRequestException()
        {
        }

        public BadRequestException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public BadRequestException(string? message)
            : base(message)
        {
            Value = new { Message = message };
            StatusCode = 400;
        }

        private BadRequestException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}