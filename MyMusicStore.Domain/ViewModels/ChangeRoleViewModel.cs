﻿using Microsoft.AspNetCore.Identity;

namespace MyMusicStore.Domain.ViewModels;

public class ChangeRoleViewModel
{
    public string UserId { get; set; }
    public string UserEmail { get; set; }
    public List<IdentityRole> AllRoles { get; set; }
    public IList<string> UserRoles { get; set; }
}