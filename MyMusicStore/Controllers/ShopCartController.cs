using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMusicStore.DAL.Interfaces;
using MyMusicStore.Domain.Helpers;
using MyMusicStore.Domain.Models;
using MyMusicStore.Domain.ViewModels;

namespace MyMusicStore.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly IOrderRepository _repository;

        public ShopCartController(IOrderRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart,
                CartTotal = CartHelper.GetCartTotal(cart)
            };
            return View(viewModel);
        }
        public async Task<IActionResult> Buy(int id)
        {
            var album = await _repository.GetAlbum(id);
            CartHelper.AddToCart(HttpContext.Session, album);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var album = await _repository.GetAlbum(id);
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
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            order.Total = CartHelper.GetCartTotal(cart);
            order.OrderDate = DateTime.Now;
            order.Email = User.Identity.Name;
            await _repository.AddOrder(order);
            await _repository.AddOrderItems(order.Id, cart);
            CartHelper.EmptyCart(HttpContext.Session);

            return RedirectToAction("Complete",
               new { id = order.Id });
        }
        [Authorize]
        public IActionResult Complete(int id)
        {
            var orders = _repository.GetAllOrders();
            bool isValid = orders.Any(
                o => o.Id == id &&
                o.Email == User.Identity.Name);

            if (isValid)
                return View(id);
            else
                return View("Error");
        }
    }
}
