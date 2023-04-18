using MyMusicStore.Domain.Models;
using MyMusicStore.Domain.ViewModels;

namespace MyMusicStore.Domain.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task AddOrderItems(int id, List<CartItem> cart);
    IQueryable<Order> GetAllOrders();
}