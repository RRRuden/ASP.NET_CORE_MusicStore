using Microsoft.AspNetCore.Identity;
using MyMusicStore.Domain.Interfaces;

namespace MyMusicStore.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        UserManager<IdentityUser> _userManager;
        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public List<IdentityUser> GetAll()
        {
            return _userManager.Users.ToList();
        }

        public async Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> AddToRolesAsync(IdentityUser user, IEnumerable<string> addedRoles)
        {
            return await _userManager.AddToRolesAsync(user, addedRoles);
        }

        public async Task<IdentityResult> RemoveFromRolesAsync(IdentityUser user, IEnumerable<string> removedRoles)
        {
            return await _userManager.RemoveFromRolesAsync(user, removedRoles);
        }

        public async Task<IdentityUser> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
