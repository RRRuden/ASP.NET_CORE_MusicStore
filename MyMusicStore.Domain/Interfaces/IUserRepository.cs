using Microsoft.AspNetCore.Identity;

namespace MyMusicStore.Domain.Interfaces;

public interface IUserRepository
{
    public List<IdentityUser> GetAll();
    public Task<IList<string>> GetRolesAsync(IdentityUser user);
    public Task<IdentityResult> AddToRolesAsync(IdentityUser user, IEnumerable<string> addedRoles);
    public Task<IdentityResult> RemoveFromRolesAsync(IdentityUser user, IEnumerable<string> removedRoles);
    public Task<IdentityUser> GetById(string id);
}