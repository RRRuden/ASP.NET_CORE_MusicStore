using System.ComponentModel.DataAnnotations;

namespace MyMusicStore.Domain.Enums;

public enum Genres
{
    [Display(Name = "Хип-хоп")] HipHop = 0,
    [Display(Name = "Рок")] Rock = 1,
    [Display(Name = "Поп-музыка")] Pop = 2
}