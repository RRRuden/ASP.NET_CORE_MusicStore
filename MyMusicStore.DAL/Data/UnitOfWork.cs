using Microsoft.AspNetCore.Identity;
using MyMusicStore.DAL.Repositories;
using MyMusicStore.Domain.Interfaces;

namespace MyMusicStore.DAL.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public UnitOfWork(ApplicationDbContext context, RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _roleManager = roleManager;
        _userManager = userManager;

        Albums = new AlbumRepository(_context);
        Artists = new ArtistRepository(_context);
        Orders = new OrderRepository(_context);
        Roles = new RoleRepository(_roleManager);
        User = new UserRepository(_userManager);
    }

    public IAlbumRepository Albums { get; }
    public IArtistRepository Artists { get; }
    public IOrderRepository Orders { get; }
    public IRoleRepository Roles { get; }
    public IUserRepository User { get; }
}