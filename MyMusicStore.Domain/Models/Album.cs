using MyMusicStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicStore.Domain.Models
{
    public class Album
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string img { get; set; }
        public int Timing { get; set; }
        public string Tracklist { get; set; }
        public Genres Genre { get; set; }
        public Artist Artist { get; set; }
    }
}
