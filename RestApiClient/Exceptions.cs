using System;
namespace RestApiClient
{
    public class ApiException : Exception
    {
        public string Uri { get; private set; }

        public string Method { get; private set; }

        public string ReasonPhrase { get; private set; }

        public ApiException(string message, string uri="", string method = "", string reason="")
            : base(message)
        {

        }
    }
}
