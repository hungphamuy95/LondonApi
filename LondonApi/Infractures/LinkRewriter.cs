using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LondonApi.Models;

namespace LondonApi.Infractures
{
    public class LinkRewriter
    {
        private readonly IUrlHelper _urlHelper;
        public LinkRewriter(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }
        public Link Rewrite(Link original)
        {
            if (original == null) return null;
            return new Link
            {
                Href = _urlHelper.Link(original.RouteName, original.RouteValues),
                Method = original.Method,
                Relations = original.Relations
            };
        }
    }
}
