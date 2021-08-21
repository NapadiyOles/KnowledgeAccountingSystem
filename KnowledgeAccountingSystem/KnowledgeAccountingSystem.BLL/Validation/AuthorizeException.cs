using System;
using System.Net;
using System.Runtime.Serialization;

namespace KnowledgeAccountingSystem.BLL.Validation
{
    [Serializable]
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
