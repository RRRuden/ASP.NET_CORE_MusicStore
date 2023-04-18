using Microsoft.EntityFrameworkCore;
using MyMusicStore.DAL.Data;
using MyMusicStore.Domain.Interfaces;
using MyMusicStore.Domain.Models;

namespace MyMusicStore.DAL.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ApplicationDbContext _context;
        public ArtistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ArtistIsExist(int? id)
        {
            return await _context.Artists.AnyAsync(a => a.Id == id);
        }

        public  async Task Create(Artist entity)
        {
            await _context.Artists.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Artist entity)
        {
            _context.Artists.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Artist> GetById(int? id)
        {
            return await _context.Artists.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Artist>> GetAll()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task Update(Artist entity)
        {
            _context.Artists.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Album>> GetArtistsAlbums(int? id)
        {
            return await _context.Albums.Where(a => a.ArtistId == id).ToListAsync();
        }
    }
}
