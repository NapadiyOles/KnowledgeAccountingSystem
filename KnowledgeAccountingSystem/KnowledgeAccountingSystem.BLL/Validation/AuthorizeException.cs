using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace KnowledgeAccountingSystem.BLL.Validation
{
    public class AuthorizeException : KnowledgeAccountException
    {
        public AuthorizeException()
        {

        }

        public AuthorizeException(string message) : base(message)
        {

        }
        public AuthorizeException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {

        }

        public AuthorizeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
