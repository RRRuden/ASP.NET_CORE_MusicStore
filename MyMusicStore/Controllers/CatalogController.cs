using Microsoft.AspNetCore.Mvc;
using MyMusicStore.Domain.Enums;
using MyMusicStore.Domain.Extensions;
using MyMusicStore.Domain.Interfaces;
using MyMusicStore.Domain.Models;

namespace MyMusicStore.Controllers;

public class CatalogController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CatalogController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index(Genres genre)
    {
        List<Album> albums;
        if (genre > 0)
        {
            albums = await _unitOfWork.Albums.GetAlbumsByGenres(genre);
            ViewBag.Genre = genre.GetName();
        }
        else
        {
            albums = await _unitOfWork.Albums.GetAlbumsIncludeArtist();
            ViewBag.Genre = "Все альбомы";
        }

        ViewBag.Genres = genre.toList();
        return View(albums);
    }
}