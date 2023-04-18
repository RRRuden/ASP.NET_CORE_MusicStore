using MyMusicStore.Domain.Models;

namespace MyMusicStore.Domain.Interfaces;

public interface IArtistRepository : IRepository<Artist>
{
    Task<bool> ArtistIsExist(int? id);

    public Task<List<Album>> GetArtistsAlbums(int? id);
    public Task<IEnumerable<Artist>> GetAll();
}