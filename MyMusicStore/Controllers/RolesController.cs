using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMusicStore.Domain.Interfaces;
using MyMusicStore.Domain.ViewModels;

namespace MyMusicStore.Controllers;

public class RolesController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public RolesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View(_unitOfWork.Roles.GetAll());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            var result = await _unitOfWork.Roles.CreateAsync(new IdentityRole(name));
            if (result.Succeeded)
                return RedirectToAction("Index");
            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(name);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var role = await _unitOfWork.Roles.FindById(id);
        await _unitOfWork.Roles.DeleteAsync(role);

        return RedirectToAction("Index");
    }

    public IActionResult UserList()
    {
        return View(_unitOfWork.User.GetAll());
    }

    public async Task<IActionResult> Edit(string userId)
    {
        var user = await _unitOfWork.User.GetById(userId);
        if (user != null)
        {
            var userRoles = await _unitOfWork.User.GetRolesAsync(user);
            var roles = _unitOfWork.Roles.GetAll();
            var model = new ChangeRoleViewModel
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = roles
            };
            return View(model);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string userId, List<string> roles)
    {
        var user = await _unitOfWork.User.GetById(userId);
        if (user == null) 
            return NotFound();

        var userRoles = await _unitOfWork.User.GetRolesAsync(user);
        var addedRoles = roles.Except(userRoles);
        var removedRoles = userRoles.Except(roles);

        await _unitOfWork.User.AddToRolesAsync(user, addedRoles);

        await _unitOfWork.User.RemoveFromRolesAsync(user, removedRoles);

        return RedirectToAction("UserList");

    }
}