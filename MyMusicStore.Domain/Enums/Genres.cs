using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicStore.Domain.Enums
{
    public enum Genres
    {
        [Display(Name = "Хип-хоп")]
        HipHop = 0,
        [Display(Name = "Инди-рок")]
        IndieRock = 1,
        [Display(Name = "Поп-музыка")]
        Pop = 2,
        [Display(Name = "К-поп")]
        Kpop = 3
    }
}
