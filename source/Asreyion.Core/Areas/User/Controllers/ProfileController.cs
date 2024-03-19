using Asreyion.Core.Areas.Account.Data;
using Asreyion.Core.Areas.Account.Models;
using Asreyion.Core.Areas.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Asreyion.Core.Areas.User.Controllers;

[Area("User")]
public class ProfileController(AuthenticationDbContext dbContext, UserManager<ApplicationUser> userManager) : Controller
{
    private readonly AuthenticationDbContext dbContext = dbContext;
    private readonly UserManager<ApplicationUser> userManager = userManager;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        string? userId = this.userManager.GetUserId(this.User);

        ApplicationUser? user = await this.dbContext.Users.FindAsync(userId);

        if (user == null)
        {
            this.ModelState.AddModelError(string.Empty, "The requested profile was not found, you are currently not logged in.");

            return base.View();
        }

        IList<string> roles = await this.userManager.GetRolesAsync(user);

        ProfileViewModel viewModel = new()
        {
            Id = user.Id,
            Username = user.Email ?? string.Empty,
            Roles = [.. roles]
        };

        return base.View(viewModel);
    }

    [HttpGet]
    public new async Task<IActionResult> View(string id)
    {
        string? userId = string.IsNullOrEmpty(id) ? this.userManager.GetUserId(this.User) : id;

        ApplicationUser? user = await this.dbContext.Users.FindAsync(userId);

        if (user == null)
        {
            this.ModelState.AddModelError(string.Empty, "The requested profile was not found.");

            return base.View("Index");
        }

        IList<string> roles = await this.userManager.GetRolesAsync(user);

        ProfileViewModel viewModel = new()
        {
            Id = user.Id,
            Username = user.Email ?? string.Empty,
            Roles = [.. roles]
        };

        return base.View("Index", viewModel);
    }
}