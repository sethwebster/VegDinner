﻿using System;
using System.Web;
using DotNetOpenAuth;

namespace NerdDinner.Services
{
    public class AppHarborEmbeddedResourceUrlService : IEmbeddedResourceRetrieval
    {
        private static string pathFormat = "{0}/Resource/GetWebResourceUrl?assemblyName={1}&typeName={2}&resourceName={3}";
        //private static string pathFormat = "{0}/Resource/GetWebResourceUrl";

        public Uri GetWebResourceUrl(Type someTypeInResourceAssembly, string manifestResourceName)
        {
            if (manifestResourceName.Contains("http"))
            {
                return new Uri(manifestResourceName);
            }
            else
            {
                var assembly = someTypeInResourceAssembly.Assembly;

                // HACK
                string completeUrl = HttpContext.Current.Request.Url.ToString();
                string host = completeUrl.Substring(0,
                    completeUrl.IndexOf(HttpContext.Current.Request.Url.AbsolutePath));

                var path = string.Format(pathFormat,
                            host,
                            HttpUtility.UrlEncode(assembly.FullName),
                            HttpUtility.UrlEncode(someTypeInResourceAssembly.ToString()),
                            HttpUtility.UrlEncode(manifestResourceName));

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

    public class EmbeddedResourceUrlService : IEmbeddedResourceRetrieval
    {
        private static string pathFormat = "{0}/Resource/GetWebResourceUrl?assemblyName={1}&typeName={2}&resourceName={3}";
        //private static string pathFormat = "{0}/Resource/GetWebResourceUrl";

        public Uri GetWebResourceUrl(Type someTypeInResourceAssembly, string manifestResourceName)
        {
            if (manifestResourceName.Contains("http"))
            {
                return new Uri(manifestResourceName);
            }
            else
            {
                var assembly = someTypeInResourceAssembly.Assembly;

                // HACK
                string completeUrl = HttpContext.Current.Request.Url.ToString();
                string host = completeUrl.Substring(0,
                    completeUrl.IndexOf(HttpContext.Current.Request.Url.AbsolutePath));

                var path = string.Format(pathFormat,
                            host,
                            HttpUtility.UrlEncode(assembly.FullName),
                            HttpUtility.UrlEncode(someTypeInResourceAssembly.ToString()),
                            HttpUtility.UrlEncode(manifestResourceName));

                var retUrl = new Uri(path);

                // If we are not operating locally, remove the port # since 
                if (retUrl.Host.ToLower() != "localhost")
                {

                }
                return new Uri(path);
            }
        }
    }
}