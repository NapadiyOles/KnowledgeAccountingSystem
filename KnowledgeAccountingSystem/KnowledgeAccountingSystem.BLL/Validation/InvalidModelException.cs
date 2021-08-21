using System;
using System.Net;
using System.Runtime.Serialization;

namespace KnowledgeAccountingSystem.BLL.Validation
{
    [Serializable]
    public class InvalidModelException : KnowledgeAccountException
    {
        public InvalidModelException()
        {

        }

        public InvalidModelException(string message) : base(message)
        {

        }
        public InvalidModelException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {

        }

        public InvalidModelException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
