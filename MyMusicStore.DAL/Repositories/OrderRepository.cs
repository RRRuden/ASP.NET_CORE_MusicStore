using Microsoft.EntityFrameworkCore;
using MyMusicStore.DAL.Data;
using MyMusicStore.Domain.Interfaces;
using MyMusicStore.Domain.Models;
using MyMusicStore.Domain.ViewModels;

namespace MyMusicStore.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddOrderItems(int OrderId, List<CartItem> cart)
        {
            foreach (var item in cart)
            {
                OrderItem Orderitem = new OrderItem
                {
                    OrderId = OrderId,
                    AlbumId = item.Album.Id,
                    Quantity = item.Quantity,
                    UnitPrice = item.Quantity * item.Album.Price
                };
                await _context.OrderItems.AddAsync(Orderitem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public IQueryable<Order> GetAllOrders()
        {
            return _context.Orders;
        }

        public async Task Create(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Order entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Order entity)
        {
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetById(int? id)
        {
            return await _context.Orders.SingleAsync(x => x.Id == id);
        }
    }
}
