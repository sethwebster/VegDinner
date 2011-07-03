using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Globalization;

namespace NerdDinner.Filters
{
    public class PortNumberFilterProvider : IFilterProvider
    {
        IList<ControllerAction> actions = new List<ControllerAction>();

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {

            throw new NotImplementedException();
        }

        internal class ControllerAction
        {
            internal string ControllerName { get; set; }
            internal string ActionName { get; set; }
        }

    }

    public class PortNumberFilterAttribute : ActionFilterAttribute
    {
        StringWriter cache = new StringWriter(CultureInfo.InvariantCulture);
        TextWriter tw = null;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            tw = filterContext.HttpContext.Response.Output;
            filterContext.HttpContext.Response.Output = cache;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var sb = cache.GetStringBuilder();
            sb.Replace(":14582", "");
            tw.WriteLine(sb.ToString());
            tw.Flush();
            cache.Dispose();
            cache = new StringWriter();
            base.OnResultExecuted(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}