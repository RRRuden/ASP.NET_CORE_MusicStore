using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMusicStore.Domain.Helpers;
using MyMusicStore.Domain.Interfaces;
using MyMusicStore.Domain.Models;
using MyMusicStore.Domain.ViewModels;

namespace MyMusicStore.Controllers;

public class ShopCartController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ShopCartController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
        var viewModel = new ShoppingCartViewModel
        {
            CartItems = cart,
            CartTotal = CartHelper.GetCartTotal(cart)
        };
        return View(viewModel);
    }

    public async Task<IActionResult> Buy(int id)
    {
        var album = await _unitOfWork.Albums.GetById(id);
        CartHelper.AddToCart(HttpContext.Session, album);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Remove(int id)
    {
        var album = await _unitOfWork.Albums.GetById(id);
        CartHelper.RemoveFromCart(HttpContext.Session, album);
        return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult Checkout()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Checkout(Order order)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
        order.Total = CartHelper.GetCartTotal(cart);
        order.OrderDate = DateTime.Now;
        order.Email = User.Identity?.Name;
        await _unitOfWork.Orders.Create(order);
        await _unitOfWork.Orders.AddOrderItems(order.Id, cart);
        CartHelper.EmptyCart(HttpContext.Session);

        return RedirectToAction("Complete",
            new { id = order.Id });
    }

    [Authorize]
    public IActionResult Complete(int id)
    {
        var orders = _unitOfWork.Orders.GetAllOrders();
        var isValid = orders.Any(
            o => User.Identity != null &&
                 o.Id == id &&
                 o.Email == User.Identity.Name);

        if (isValid)
            return View(id);
        return View("Error");
    }
}