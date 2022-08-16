using Microsoft.AspNetCore.Mvc;
using MyMusicStore.DAL.Interfaces;
using MyMusicStore.Domain.ViewModels;

namespace MyMusicStore.Controllers
{
    public class ArtistsPageController : Controller
    {
        private readonly IArtistRepository _repository;

        public ArtistsPageController(IArtistRepository artistRepository)
        {
            _repository = artistRepository;
        }

        public async Task<IActionResult> Index()
        {
            var artists = await _repository.GetAll();
            return View(artists);
        }

        public async Task<IActionResult> Albums(int id)
        {
            var artists = await _repository.Get(id);
            var albumsList = await _repository.GetArtistsAlbums(id);

            var artistsPageViewModel = new ArtistsPageViewModel
            {
                Name = artists.Name,
                img = artists.img,
                albums = albumsList
            };

            return View(artistsPageViewModel);
        }

        public async Task<IActionResult> Details(int? ArtistId, int? AlbumId)
        {
            if (ArtistId == null || AlbumId == null)
            {
                return NotFound();
            }
            var albums = await _repository.GetArtistsAlbums(ArtistId);
            var album = albums.FirstOrDefault(x => x.Id == AlbumId);
            album.Artist = await _repository.Get(ArtistId);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }
    }
}
