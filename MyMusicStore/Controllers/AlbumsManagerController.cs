using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMusicStore.DAL.Interfaces;
using MyMusicStore.Domain.Enums;
using MyMusicStore.Domain.Models;
using System.Linq;

namespace MyMusicStore.Controllers
{
    public class AlbumsManagerController : Controller
    {
        private readonly IAlbumRepository _repository;

        public AlbumsManagerController(IAlbumRepository albumRepository)
        {
            _repository = albumRepository;
        }

        public async Task<IActionResult> Index()
        {
            var albums = await _repository.GetAlbumsIncludeArtist();
            return View(albums);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Artists = new SelectList(await _repository.GetArtists(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Genre,ArtistId,Title,ReleaseDate,Price,img,Tracklist,Timing")] Album album)
        {
            await _repository.Create(album);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _repository.Get(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(await _repository.GetArtists(), "Id", "Name", album.ArtistId);
            return View(album);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Genre,ArtistId,Title,ReleaseDate,Price,img,Tracklist, Timing")] Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }
            try
            {
                await _repository.Update(album);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.IsAlbumExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _repository.GetAlbumIncludeArtist(id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _repository.Get(id);
            await _repository.Delete(album);
            return RedirectToAction(nameof(Index));
        }
    }
}
