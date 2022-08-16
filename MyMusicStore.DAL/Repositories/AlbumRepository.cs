using Microsoft.EntityFrameworkCore;
using MyMusicStore.DAL.Interfaces;
using MyMusicStore.Domain.Enums;
using MyMusicStore.Domain.Models;

namespace MyMusicStore.DAL.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ApplicationDbContext _context;

        public AlbumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Album entity)
        {
            await _context.Albums.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Album entity)
        {
            _context.Albums.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Album> Get(int? id)
        {
            return await _context.Albums.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Album>> GetAll()
        {
            return await _context.Albums.ToListAsync();
        }

        public async Task<Album> Update(Album entity)
        {
            _context.Albums.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public Task<List<Album>> GetAlbumsByArtist(int id)
        {
            return _context.Albums.Where(x => x.ArtistId == id).ToListAsync();
        }

        public Task<List<Album>> GetAlbumsByGenres(Genres genre)
        {
            return _context.Albums.Include(a => a.Artist).Where(x=> x.Genre == genre).ToListAsync();
        }

        public async Task<bool> IsAlbumExist(int id)
        {
            return await _context.Albums.AnyAsync(a => a.Id == id);
        }

        public async Task<List<Artist>> GetArtists()
        {
            return await _context.Artists.ToListAsync();
        }
        public async Task<Album> GetAlbumIncludeArtist(int? id)
        {
            return await _context.Albums.Include(a => a.Artist).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Album>> GetAlbumsIncludeArtist()
        {
            return await _context.Albums.Include(a => a.Artist).ToListAsync();
        }
    }
}
