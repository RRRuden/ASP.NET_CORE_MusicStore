using MyMusicStore.Domain.Models;

namespace MyMusicStore.Domain.ViewModels;

public class ArtistsPageViewModel
{
    public string Name { get; set; }
    public string Image { get; set; }
    public IEnumerable<Album> Albums{ get; set; }
}