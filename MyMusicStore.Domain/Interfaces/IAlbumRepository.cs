using MyMusicStore.Domain.Enums;
using MyMusicStore.Domain.Models;

namespace MyMusicStore.Domain.Interfaces;

public interface IAlbumRepository : IRepository<Album>
{
    public Task<List<Album>> GetAlbumsByArtist(int id);

    public Task<Album> GetAlbumIncludeArtist(int? id);

    public Task<List<Album>> GetAlbumsIncludeArtist();

    public Task<List<Artist>> GetAll();

    public Task<List<Album>> GetAlbumsByGenres(Genres genre);

    public Task<bool> IsAlbumExist(int id);
}