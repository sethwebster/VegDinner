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
            return ResolveUri(retUrl);
        }

        public static Uri ResolveUri(Uri path)
        {
            Uri retUrl = path;
            if (path.Host.ToLower() != "localhost")
            {
                retUrl = new Uri(
                    retUrl.Scheme + "://" + retUrl.Host + retUrl.PathAndQuery
               );
            }

            return retUrl;
        }
    }
}