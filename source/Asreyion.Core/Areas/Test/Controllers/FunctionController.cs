using Asreyion.Core.Areas.Test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Asreyion.Core.Areas.Test.Controllers;

[Area("Test")]
public class FunctionController : Controller
{
    private readonly IUrlHelper urlHelper;
    private readonly ILogger<FunctionController> logger;

    public FunctionController(IUrlHelper urlHelper, ILogger<FunctionController> logger)
    {
        this.urlHelper = urlHelper;
        this.logger = logger;

        // Generate URL for the Index action of the FunctionController
        string? url = this.urlHelper.Action("Index", "Function", new { area = "Test" });

        // You can use the generated URL as needed, for example, in a redirect or to pass it to a view
        this.logger.LogWarning($"URL for Index action: {url}");
    }

    public IActionResult Index()
    {
        // Generate URL for the Index action of the FunctionController
        string? url = this.urlHelper.Action("Index", "Function", new { area = "Test" });

        return this.View(new TestModel(url ?? ""));
    }
}
