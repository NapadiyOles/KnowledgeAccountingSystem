using System;
using System.Net;
using System.Runtime.Serialization;

namespace KnowledgeAccountingSystem.BLL.Validation
{
    [Serializable]
    public class ResourceAlreadyExistException : KnowledgeAccountException
    {
        public ResourceAlreadyExistException()
        {

        }

        public ResourceAlreadyExistException(string message) : base(message)
        {

        }
        public ResourceAlreadyExistException(string message, HttpStatusCode statusCode) : base(message, statusCode)
        {

        }

        public ResourceAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
