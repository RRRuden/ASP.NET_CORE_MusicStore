using MyMusicStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicStore.Domain.ViewModels
{
    public class ArtistsPageViewModel
    {
        public string Name { get; set; }
        public string img { get; set; }
        public IEnumerable<Album> albums { get; set; }
    }
}
