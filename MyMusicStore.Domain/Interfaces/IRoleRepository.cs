using Microsoft.AspNetCore.Identity;

namespace MyMusicStore.Domain.Interfaces;

public interface IRoleRepository
{
    public List<IdentityRole> GetAll();
    public Task<IdentityResult> CreateAsync(IdentityRole role);
    public Task<IdentityRole> FindById(string id);
    public Task<IdentityResult> DeleteAsync(IdentityRole role);
}