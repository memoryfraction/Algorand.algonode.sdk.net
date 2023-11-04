using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Algorand.algonode.sdk.net.Models
{
    public class AlgorandApiException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public AlgorandApiException(HttpStatusCode statusCode)
            : base($"Request unsuccessful: {(int)statusCode} ({statusCode})")
        {
            StatusCode = statusCode;
        }
    }
}
