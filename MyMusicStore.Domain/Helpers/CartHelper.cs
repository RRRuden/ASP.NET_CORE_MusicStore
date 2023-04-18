using Microsoft.AspNetCore.Http;
using MyMusicStore.Domain.Models;
using MyMusicStore.Domain.ViewModels;

namespace MyMusicStore.Domain.Helpers;

public static class CartHelper
{
    public static void AddToCart(ISession session, Album album)
    {
        var cart = session.GetObjectFromJson<List<CartItem>>("cart");
        if (cart == null)
        {
            cart = new List<CartItem>();
            cart.Add(new CartItem { Album = album, Quantity = 1 });
        }
        else
        {
            var index = GetIndex(cart, album.Id);
            if (index >= 0)
                cart[index].Quantity++;
            else
                cart.Add(new CartItem { Album = album, Quantity = 1 });
        }

        session.SetObjectAsJson("cart", cart);
    }

    public static void RemoveFromCart(ISession session, Album album)
    {
        var cart = session.GetObjectFromJson<List<CartItem>>("cart");
        var index = GetIndex(cart, album.Id);
        if (cart[index].Quantity > 1)
            cart[index].Quantity--;
        else
            cart.RemoveAt(index);

        session.SetObjectAsJson("cart", cart);
    }

    public static int GetIndex(List<CartItem> cart, int AlbumId)
    {
        for (var i = 0; i < cart.Count; i++)
            if (cart[i].Album.Id == AlbumId)
                return i;
        return -1;
    }

    public static void EmptyCart(ISession session)
    {
        var cart = session.GetObjectFromJson<List<CartItem>>("cart");
        cart.Clear();
        session.SetObjectAsJson("cart", cart);
    }

    public static decimal GetCartTotal(List<CartItem> cart)
    {
        if (cart != null)
            return cart.Sum(item => item.Album.Price * item.Quantity);
        return 0;
    }
}