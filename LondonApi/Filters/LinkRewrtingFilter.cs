using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
using System.Reflection;
using LondonApi.Infractures;

namespace LondonApi.Filters
{
    public class LinkRewrtingFilter : IAsyncResultFilter
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        public LinkRewrtingFilter(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var asObjectResult = context.Result as Microsoft.AspNetCore.Mvc.ObjectResult;
            bool shouldSkip = asObjectResult?.Value == null || asObjectResult?.StatusCode != (int)HttpStatusCode.OK;
            if (shouldSkip)
            {
                await next();
                return;
            }
            var rewriter = new LinkRewriter(_urlHelperFactory.GetUrlHelper(context));
        }
        //private static void RewriteAllLinks(object model, LinkRewriter rewriter)
        //{
        //    if (model == null) return;
        //    var allProps=model.GetType().GetTypeInfo()
        //        .
        //}
    }
}
