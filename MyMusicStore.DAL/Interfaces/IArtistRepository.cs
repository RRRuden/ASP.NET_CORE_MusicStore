using MyMusicStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicStore.DAL.Interfaces
{
    public interface IArtistRepository:IRepository<Artist>
    {
        Task<bool> ArtistIsExist(int? id);

        public Task<List<Album>> GetArtistsAlbums(int? id);
    }
}
