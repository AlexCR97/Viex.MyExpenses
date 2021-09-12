using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Viex.MyExpenses.Domain
{
    public class DomainException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public DomainException()
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public DomainException(string message)
            : base(message)
        {
            StatusCode = HttpStatusCode.InternalServerError;
        }
    }
}
