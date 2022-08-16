using MyMusicStore.Domain.Enums;
using MyMusicStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicStore.DAL.Interfaces
{
    public interface IAlbumRepository:IRepository<Album>
    {
        public Task<List<Album>> GetAlbumsByArtist(int id);

        public Task<Album> GetAlbumIncludeArtist(int? id);

        public Task<List<Album>> GetAlbumsIncludeArtist();

        public Task<List<Artist>> GetArtists();

        public Task<List<Album>> GetAlbumsByGenres(Genres genre);

        public Task<bool> IsAlbumExist(int id);
    }
}
