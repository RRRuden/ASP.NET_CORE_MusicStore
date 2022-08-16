using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicStore.DAL.Interfaces;
using MyMusicStore.DAL.Repositories;
using MyMusicStore.Domain.Models;

namespace MyMusicStore.Controllers
{
    public class ArtistManagerController : Controller
    {
        private readonly IArtistRepository _repository;

        public ArtistManagerController(IArtistRepository artistRepository)
        {
            _repository = artistRepository;
        }

        public async Task<IActionResult> Index()
        {
            var artists = await _repository.GetAll();
            return View(artists);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Artist artist)
        {
            await _repository.Create(artist);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artist = await _repository.Get(id);
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Artist artist)
        {
            if (id != artist.Id)
            {
                return NotFound();
            }

            try
            {
                await _repository.Update(artist);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ArtistIsExist( id))
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

            var artist = await _repository.Get(id);
            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await _repository.Get(id);
            await _repository.Delete(artist);
            return RedirectToAction(nameof(Index));
        }
    }
}
