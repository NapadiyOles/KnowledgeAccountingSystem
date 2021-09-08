using KnowledgeAccountingSystem.BLL.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace KnowledgeAccountingSystem.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = context.Exception switch
            {
                ArgumentNullException => new NotFoundObjectResult(context.Exception.Message),
                AuthorizeException => new BadRequestObjectResult(context.Exception.Message),
                InvalidModelException => new BadRequestObjectResult(context.Exception.Message),
                ResourceAlreadyExistException => new BadRequestObjectResult(context.Exception.Message),
                KnowledgeAccountException => new BadRequestObjectResult(context.Exception.Message),
                ArgumentException => new BadRequestObjectResult(context.Exception.Message),
                _ => new BadRequestObjectResult(
                    $"Unhandled error occured. {context.Exception}: {context.Exception.Message}")
            };
            base.OnException(context); 
        }
    }
}
