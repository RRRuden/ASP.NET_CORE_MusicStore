namespace MyMusicStore.Domain.Interfaces;

public interface IUnitOfWork
{
    IAlbumRepository Albums { get; }
    IArtistRepository Artists { get; }
    IOrderRepository Orders { get; }
    IRoleRepository Roles { get; }
    IUserRepository User { get; }
}