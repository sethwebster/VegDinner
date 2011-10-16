Imports System.Linq
Imports System.Web.Mvc
Imports $rootnamespace$.Microsoft.Web.Mvc
 
<Assembly: WebActivator.PreApplicationStartMethod(GetType($rootnamespace$.App_Start.MobileViewEngines), "Start")>
Namespace $rootnamespace$.App_Start
	Public NotInheritable Class MobileViewEngines
		Public Shared Sub Start()
			ViewEngines.Engines.Remove(ViewEngines.Engines.OfType(Of RazorViewEngine)().First())
			ViewEngines.Engines.Add(New MobileCapableRazorViewEngine())
			ViewEngines.Engines.Remove(ViewEngines.Engines.OfType(Of WebFormViewEngine)().First())
			ViewEngines.Engines.Add(New MobileCapableWebFormViewEngine())
		End Sub
	End Class
End Namespace
