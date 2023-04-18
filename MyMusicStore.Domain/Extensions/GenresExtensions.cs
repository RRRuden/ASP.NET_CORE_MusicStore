using System.ComponentModel.DataAnnotations;
using System.Reflection;
using MyMusicStore.Domain.Enums;

namespace MyMusicStore.Domain.Extensions;

public static class GenresExtensions
{
    public static string GetName(this Genres genre)
    {
        return genre.GetType()
            .GetMember(genre.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName();
    }

    public static List<Genres> toList(this Genres genres)
    {
        return Enum.GetValues(typeof(Genres))
            .Cast<Genres>()
            .ToList();
    }
}