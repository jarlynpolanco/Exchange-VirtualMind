using System;
using System.Net;
using System.Runtime.Serialization;

namespace Exchange.Core.Exceptions
{

    [Serializable]
    public sealed class HttpStatusException : Exception
    {
        public HttpStatusCode? StatusCode { get; set; }

        private HttpStatusException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public HttpStatusException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
