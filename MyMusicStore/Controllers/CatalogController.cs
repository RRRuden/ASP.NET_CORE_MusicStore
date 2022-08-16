using Microsoft.AspNetCore.Mvc;
using MyMusicStore.DAL.Interfaces;
using MyMusicStore.Domain.Enums;
using MyMusicStore.Domain.Extensions;
using MyMusicStore.Domain.Models;

namespace MyMusicStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IAlbumRepository _repository;

        public CatalogController(IAlbumRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(Genres genre)
        {
            List<Album> albumsList;
            if (genre > 0)
            {
                albumsList = await _repository.GetAlbumsByGenres(genre);
                ViewBag.Genre = genre.GetName();
            }
            else
            {
                albumsList = await _repository.GetAlbumsIncludeArtist();
                ViewBag.Genre = "Все альбомы";
            }
            ViewBag.Genres = genre.toList();
            return View(albumsList);
        }
    }
}
