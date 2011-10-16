using System.Linq;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
 
[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.MobileViewEngines), "Start")]
namespace $rootnamespace$.App_Start {
    public static class MobileViewEngines{
        public static void Start() 
        {
			ViewEngines.Engines.Remove(ViewEngines.Engines.OfType<RazorViewEngine>().First());
			ViewEngines.Engines.Add(new MobileCapableRazorViewEngine());
			ViewEngines.Engines.Remove(ViewEngines.Engines.OfType<WebFormViewEngine>().First());
			ViewEngines.Engines.Add(new MobileCapableWebFormViewEngine());
        }
    }
}