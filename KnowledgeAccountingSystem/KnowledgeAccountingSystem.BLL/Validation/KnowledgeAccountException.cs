using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace KnowledgeAccountingSystem.BLL.Validation
{
    [Serializable]
    public class KnowledgeAccountException : Exception
    {
        protected readonly HttpStatusCode code;
        public KnowledgeAccountException() : base()
        {

        }

        public KnowledgeAccountException(string message) : base(message)
        {

        }
        public KnowledgeAccountException(string message, HttpStatusCode statusCode) : base(message)
        {
            code = statusCode;
        }

        public KnowledgeAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

    }
}
