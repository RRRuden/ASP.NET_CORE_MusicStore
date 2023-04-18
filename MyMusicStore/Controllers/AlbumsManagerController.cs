using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMusicStore.Domain.Interfaces;
using MyMusicStore.Domain.Models;

namespace MyMusicStore.Controllers;

public class AlbumsManagerController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public AlbumsManagerController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        var albums = await _unitOfWork.Albums.GetAlbumsIncludeArtist();
        return View(albums);
    }

    public async Task<IActionResult> Create()
    {
        var artists = await _unitOfWork.Artists.GetAll();
        ViewBag.Artists = new SelectList(artists, "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [Bind("Id,Genre,ArtistId,Title,ReleaseDate,Price,Image,Tracklist,Timing")] Album album)
    {
        await _unitOfWork.Albums.Create(album);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var album = await _unitOfWork.Albums.GetById(id);

        var albums = await _unitOfWork.Artists.GetAll();

        ViewData["ArtistId"] = new SelectList(albums, "Id", "Name", album.ArtistId);
        return View(album);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Genre,ArtistId,Title,ReleaseDate,Price,Image,Tracklist, Timing")] Album album)
    {
        if (id != album.Id) return NotFound();
        try
        {
            await _unitOfWork.Albums.Update(album);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _unitOfWork.Albums.IsAlbumExist(id))
                return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var album = await _unitOfWork.Albums.GetAlbumIncludeArtist(id);

        return View(album);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var album = await _unitOfWork.Albums.GetById(id);
        await _unitOfWork.Albums.Delete(album);
        return RedirectToAction(nameof(Index));
    }
}