Imports System.IO
Imports System.Web.Mvc

Namespace Microsoft.Web.Mvc
    Public Class MobileCapableWebFormViewEngine
        Inherits WebFormViewEngine

        Public Overrides Function FindView (controllerContext As ControllerContext, viewName As String, _
                                            masterName As String, useCache As Boolean) As ViewEngineResult
            Dim _
                overrideViewName As String = _
                    If(controllerContext.HttpContext.Request.Browser.IsMobileDevice, viewName & ".Mobile", viewName)
            Dim result As ViewEngineResult = NewFindView (controllerContext, overrideViewName, masterName, useCache)

            ' If we're looking for a Mobile view and couldn't find it try again without modifying the viewname
            If overrideViewName.Contains (".Mobile") AndAlso (result Is Nothing OrElse result.View Is Nothing) Then
                result = NewFindView (controllerContext, viewName, masterName, useCache)
            End If
            Return result
        End Function

        Private Function NewFindView (controllerContext As ControllerContext, viewName As String, masterName As String, _
                                      useCache As Boolean) As ViewEngineResult
            ' Get the name of the controller from the path
            Dim controller As String = controllerContext.RouteData.Values ("controller").ToString()
            Dim area As String = ""
            Try
                area = controllerContext.RouteData.DataTokens ("area").ToString()
            Catch
            End Try

            ' Create the key for caching purposes           
            Dim keyPath As String = Path.Combine (area, controller, viewName)

            ' Try the cache           
            If useCache Then
                'If using the cache, check to see if the location is cached.               
                Dim cacheLocation As String = ViewLocationCache.GetViewLocation (controllerContext.HttpContext, keyPath)
                If Not String.IsNullOrWhiteSpace (cacheLocation) Then
                    Return New ViewEngineResult (CreateView (controllerContext, cacheLocation, masterName), Me)
                End If
            End If

            ' Remember the attempted paths, if not found display the attempted paths in the error message.           
            Dim attempts = New List(Of String)()

            Dim _
                locationFormats As String() = _
                    If(String.IsNullOrEmpty (area), ViewLocationFormats, AreaViewLocationFormats)

            ' for each of the paths defined, format the string and see if that path exists. When found, cache it.           
            For Each rootPath As String In locationFormats
                Dim _
                    currentPath As String = _
                        If _
                        (String.IsNullOrEmpty (area), String.Format (rootPath, viewName, controller), _
                         String.Format (rootPath, viewName, controller, area))

                If FileExists (controllerContext, currentPath) Then
                    ViewLocationCache.InsertViewLocation (controllerContext.HttpContext, keyPath, currentPath)

                    Return New ViewEngineResult (CreateView (controllerContext, currentPath, masterName), Me)
                End If

                ' If not found, add to the list of attempts.               
                attempts.Add (currentPath)
            Next

            ' if not found by now, simply return the attempted paths.           
            Return New ViewEngineResult (attempts.Distinct().ToList())
        End Function
    End Class
End Namespace