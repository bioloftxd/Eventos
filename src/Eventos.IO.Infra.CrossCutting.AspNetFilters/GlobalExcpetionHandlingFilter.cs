using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace Eventos.IO.Infra.CrossCutting.AspNetFilters
{
    public class GlobalExcpetionHandlingFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExcpetionHandlingFilter> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public GlobalExcpetionHandlingFilter(ILogger<GlobalExcpetionHandlingFilter> logger, 
            IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            if (_hostingEnvironment.IsProduction())
            {
                _logger.LogError(1, context.Exception, context.Exception.Message);
            }

            var result = new ViewResult {ViewName = "Error"};
            var modelData = new EmptyModelMetadataProvider();
            result.ViewData = new ViewDataDictionary(modelData, context.ModelState)
            {
                {"MensagemErro", context.Exception.Message }
            };

            context.ExceptionHandled = true;
            context.Result = result;
        }
    }
}