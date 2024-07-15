using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HotelWebDemo.Services;

public class ViewRenderService : IViewRenderService
{
    private readonly IRazorViewEngine razorViewEngine;
    private readonly ITempDataProvider tempDataProvider;
    private readonly IServiceProvider serviceProvider;
    private readonly IActionContextAccessor actionContextAccessor;

    public ViewRenderService(
        IRazorViewEngine razorViewEngine,
        ITempDataProvider tempDataProvider,
        IServiceProvider serviceProvider,
        IActionContextAccessor actionContextAccessor)
    {
        this.razorViewEngine = razorViewEngine;
        this.tempDataProvider = tempDataProvider;
        this.serviceProvider = serviceProvider;
        this.actionContextAccessor = actionContextAccessor;
    }

    public async Task<string> RenderToStringAsync(string viewName, object model)
    {
        DefaultHttpContext? httpContext = new()
        {
            RequestServices = serviceProvider
        };
        ActionContext? actionContext = new(httpContext, new RouteData(), new ActionDescriptor());

        ViewEngineResult? viewEngineResult = razorViewEngine.FindView(actionContextAccessor.ActionContext, viewName, false);
        if (!viewEngineResult.Success)
        {
            throw new InvalidOperationException($"Unable to find view '{viewName}'");
        }

        IView view = viewEngineResult.View;
        using StringWriter stringWriter = new();
        ViewContext? viewContext = new(
            actionContext,
            view,
            new ViewDataDictionary<object>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            },
            new TempDataDictionary(actionContext.HttpContext, tempDataProvider),
            stringWriter,
            new HtmlHelperOptions()
        );

        await view.RenderAsync(viewContext);

        return stringWriter.ToString();
    }
}
