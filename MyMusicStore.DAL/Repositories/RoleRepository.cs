using Microsoft.AspNetCore.Identity;
using MyMusicStore.Domain.Interfaces;

namespace MyMusicStore.DAL.Repositories
{
    public class RoleRepository:IRoleRepository
    {
        RoleManager<IdentityRole> _roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        public List<IdentityRole> GetAll()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<IdentityResult> CreateAsync(IdentityRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityRole> FindById(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> DeleteAsync(IdentityRole role)
        {
            return await _roleManager.DeleteAsync(role);
        }
    }
}
