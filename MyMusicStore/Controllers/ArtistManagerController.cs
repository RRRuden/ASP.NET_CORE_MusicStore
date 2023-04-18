using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicStore.Domain.Interfaces;
using MyMusicStore.Domain.Models;

namespace MyMusicStore.Controllers;

public class ArtistManagerController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ArtistManagerController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> Index()
    {
        var artists = await _unitOfWork.Artists.GetAll();
        return View(artists);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Artist artist)
    {
        await _unitOfWork.Artists.Create(artist);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var artist = await _unitOfWork.Artists.GetById(id);
        return View(artist);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Artist artist)
    {
        if (id != artist.Id) return NotFound();

        try
        {
            await _unitOfWork.Artists.Update(artist);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _unitOfWork.Artists.ArtistIsExist(id))
                return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var artist = await _unitOfWork.Artists.GetById(id);

        return View(artist);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var artist = await _unitOfWork.Artists.GetById(id);
        await _unitOfWork.Artists.Delete(artist);
        return RedirectToAction(nameof(Index));
    }
}