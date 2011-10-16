using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerdDinner.Services
{
    public static class AppHarborUriHelper
    {
        public static Uri CurrentUri
        {
            get
            {
                return ResolveUri(HttpContext.Current.Request.RawUrl);
            }
        }

        public static Uri ResolveUri(string path)
        {
            var retUrl = new Uri(path);

            // If we are not operating locally, remove the port # since 
            if (retUrl.Host.ToLower() != "localhost")
            {
                retUrl = new Uri(
                    retUrl.Scheme + "://" + retUrl.Host + retUrl.PathAndQuery
               );
            }
            return retUrl;
        }
    }
}