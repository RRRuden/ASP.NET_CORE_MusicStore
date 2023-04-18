namespace MyMusicStore.Domain.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int AlbumId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public virtual Album? Album { get; set; }
    public virtual Order? Order { get; set; }
}