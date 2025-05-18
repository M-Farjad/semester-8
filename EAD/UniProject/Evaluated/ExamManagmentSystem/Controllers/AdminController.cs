using ExamManagmentSystem.Helpers;
using ExamManagmentSystem.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class AdminController : BaseController
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    // View all roles
    [Authorize(Policy = "Permission.ManageRoles")]
    public async Task<IActionResult> ManageRoles()
    {
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }

    // GET: Create new role
    [Authorize(Policy = "Permission.ManageRoles")]
    public IActionResult CreateRole()
    {
        return View();
    }

    // POST: Create new role
    [Authorize(Policy = "Permission.ManageRoles")]
    [HttpPost]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        if (!string.IsNullOrEmpty(roleName))
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
                return RedirectToAction("ManageRoles");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        return View();
    }

    // Edit Role (Optional)
    [Authorize(Policy = "Permission.ManageRoles")]
    [HttpGet]
    public async Task<IActionResult> EditRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null) return NotFound();

        var roleClaims = await _roleManager.GetClaimsAsync(role);

        var model = new EditRolePermissionsViewModel
        {
            RoleId = role.Id,
            RoleName = role.Name,
            Permissions = RolePermissions.AllPermissions.Select(p => new PermissionCheckbox
            {
                Name = p,
                IsSelected = roleClaims.Any(c => c.Type == "Permission" && c.Value == p)
            }).ToList()
        };

        return View(model);
    }

    [Authorize(Policy = "Permission.ManageRoles")]
    [HttpPost]
    public async Task<IActionResult> EditRole(EditRolePermissionsViewModel model)
    {
        var role = await _roleManager.FindByIdAsync(model.RoleId);
        if (role == null) return NotFound();

        var currentClaims = await _roleManager.GetClaimsAsync(role);

        // Remove existing permission claims
        foreach (var claim in currentClaims.Where(c => c.Type == "Permission"))
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }

        // Add selected ones
        foreach (var permission in model.Permissions.Where(p => p.IsSelected))
        {
            await _roleManager.AddClaimAsync(role, new Claim("Permission", permission.Name));
        }

        TempData["Success"] = "Role responsibilities updated.";
        return RedirectToAction("ManageRoles");
    }

    // Delete Role
    [Authorize(Policy = "Permission.ManageRoles")]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        await _roleManager.DeleteAsync(role);
        return RedirectToAction("ManageRoles");
    }
}
