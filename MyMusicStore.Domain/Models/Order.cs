using System.ComponentModel.DataAnnotations;

namespace MyMusicStore.Domain.Models;

public class Order
{
    public int Id { get; set; }

    [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
        ErrorMessage = "Email is is not valid.")]
    public string? Email { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }

    [StringLength(24)] 
    public string? Phone { get; set; }

    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem>? OrderDetails { get; set; }
}