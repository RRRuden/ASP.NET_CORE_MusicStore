using MyMusicStore.Domain.Models;

namespace MyMusicStore.Domain.ViewModels
{
    public class CartItem
    {
        public Album? Album { get; set; }
        public int Quantity { get; set; }
    }
}
