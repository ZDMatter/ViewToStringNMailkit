MAilKit:

1. Signup on site https://ethereal.email/ for fake SMTP service. Which allows to setup SMTP and a fake email process check.
2. Add Nuget Package MAilKit 3.4.1  by Jeffrey Stedfast; Microsoft also suggest to use this one rather than System.Net
3. Add respective generated email address and password in step 1 in AsppSettings.json or in code directly for testing purpose.
4. Test ViewStringController -> EmployeeHtmlEmail()
5. Generic Service component is available under Services -> EmailService.cs
6. Check email on same etherel.email infterface under message

To Geenerate HTML string from/of a View:
1. Add Interface IViewRendererService 
2. In ViewRendererService by using RazorViewEngine setup to write ViewEngineResult in stream writer which process as a string output.
3. ViewEngine.FindView() Find the respective View in Controller, View Name and Model
	e.g. var emailHtmlFromView = await this._renderer.RenderAsync(this, "Index", GetEmployees());
4. Write ViewContext in stream
5. String can be further use as respective HTML rendered.

Reference:
IRazorViewEngine Interface (Microsoft.AspNetCore.Mvc.Razor) | Microsoft Learn:
https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razor.irazorviewengine?view=aspnetcore-6.0

RazorViewEngine Class (Microsoft.AspNetCore.Mvc.Razor) | Microsoft Learn:
https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razor.razorviewengine?view=aspnetcore-6.0

RazorViewEngine.FindView(ActionContext, String, Boolean) Method (Microsoft.AspNetCore.Mvc.Razor) | Microsoft Learn:
https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.razor.razorviewengine.findview?view=aspnetcore-6.0


