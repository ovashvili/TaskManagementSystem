using MediatR;
using System;
using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.Infrastructure.Enums;
using Task = System.Threading.Tasks.Task;
using TaskManagementSystem.Application.Contracts;
using AutoMapper;
using TaskManagementSystem.Infrastructure.Models.Entities;
using TaskManagementSystem.Application.User.Commands.RegisterUser;

namespace TaskManagementSystem.Infrastructure.Context;

public class ContextSeed
{
    public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Task_Create.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Task_Delete.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Task_Update.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Task_View.ToString()));
    }

    public static async Task SeedSudoAsync(IUserService userService, UserManager<User> userManager)
    {
          if (!await userService.AnyAsync())
          {
              var user = new RegisterUserCommandModel() { FirstName = "Nikusha", LastName = "Ovashvili", UserName = "nika12", Password = "Qwer123$"};
              var response = await userService.RegisterAsync(user);
              var adminUser = await userManager.FindByIdAsync(response.Data!.Id);
              await userManager.AddToRolesAsync(adminUser, new List<string> { Roles.Basic.ToString(), Roles.Moderator.ToString(), Roles.Admin.ToString() });
          }
    }
}
