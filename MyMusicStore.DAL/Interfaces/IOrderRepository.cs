using MyMusicStore.Domain.Models;
using MyMusicStore.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicStore.DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task<Album> GetAlbum(int id);
        Task<bool> AddOrder(Order order);
        Task<bool> AddOrderItems(int id, List<CartItem> cart);
        IQueryable<Order> GetAllOrders();

    }
}
