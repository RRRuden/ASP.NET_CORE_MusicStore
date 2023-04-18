using Microsoft.AspNetCore.Mvc;
using MyMusicStore.Domain.Interfaces;
using MyMusicStore.Domain.ViewModels;

namespace MyMusicStore.Controllers;

public class ArtistsPageController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ArtistsPageController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        var artists = await _unitOfWork.Artists.GetAll();
        return View(artists);
    }

    public async Task<IActionResult> Albums(int id)
    {
        var artist = await _unitOfWork.Artists.GetById(id);
        var albums = await _unitOfWork.Artists.GetArtistsAlbums(id);

        var artistsPageViewModel = new ArtistsPageViewModel
        {
            Name = artist.Name,
            Image = artist.Image,
            Albums = albums
        };

        return View(artistsPageViewModel);
    }

    public async Task<IActionResult> Details(int? artistId, int? albumId)
    {
        var albums = await _unitOfWork.Artists.GetArtistsAlbums(artistId);
        var album = albums.FirstOrDefault(x => x.Id == albumId);
        if (album != null)
            album.Artist = await _unitOfWork.Artists.GetById(artistId);
        
        return View(album);
    }
}