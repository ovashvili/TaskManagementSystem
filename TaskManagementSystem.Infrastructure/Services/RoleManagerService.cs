using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using TaskManagementSystem.Application.Common.Enums;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Models.Entities;

namespace TaskManagementSystem.Infrastructure.Services
{
    public class RoleManagerService : IRoleManagerService
    {
        private readonly AppDBContext _db;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public RoleManagerService(AppDBContext db, UserManager<User> userManager, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CommandResponse<string>> AddAsync(string roleName)
        {
            if (_db.Roles.Any(x => x.Name == roleName))
                return new CommandResponse<string>(StatusCode.Conflict, "Role '" + roleName + "' does already exist");

            await _db.Roles.AddAsync(new IdentityRole(roleName.Trim()));
            await _db.SaveChangesAsync();

            return new CommandResponse<string>(StatusCode.Success);
        }

        public async Task<CommandResponse<string>> DeleteAsync(string roleName)
        {
            var role = await _db.Roles.SingleOrDefaultAsync(x => x.Name == roleName);

            if (role == null)
                return new CommandResponse<string>(StatusCode.NotFound, "Role '" + roleName + "' does not exist");

            _db.Roles.Remove(role);
            await _db.SaveChangesAsync();

            return new CommandResponse<string>(StatusCode.Success);
        }

        public async Task<CommandResponse<string>> AddRoleToUserAsync(string userId, string roleName)
        {
            if (!_db.Roles.Any(x => x.Name == roleName))
                return new CommandResponse<string>(StatusCode.NotFound, "Role '" + roleName + "' does not exist");

            var user = await _db.Users.FindAsync(userId);

            if (user == null)
                return new CommandResponse<string>(StatusCode.NotFound, "User not found");

            var mappedUser = _mapper.Map<User>(user);

            await _userManager.AddToRoleAsync(mappedUser, roleName);

            return new CommandResponse<string>(StatusCode.Success);
        }

        public async Task<CommandResponse<IEnumerable<RoleDto>>> GetRolesAsync()
        {
            var roles = await _db.Roles.ToListAsync();

            if(!roles.Any())
                return new CommandResponse<IEnumerable<RoleDto>>(StatusCode.NotFound, "Roles could not be found");

            var mappedRoles = _mapper.Map<IEnumerable<RoleDto>>(roles);

            return new CommandResponse<IEnumerable<RoleDto>>(StatusCode.Success, null, mappedRoles);
        }

        public async Task<CommandResponse<IEnumerable<string>>> GetUserRolesAsync(string userId)
        {
            var user = await _db.Users.FindAsync(userId);

            if (user == null)
                return new CommandResponse<IEnumerable<string>>(StatusCode.NotFound, "User could not be found.");

            var userRoles = await _userManager.GetRolesAsync(user);

            if (!userRoles.Any())
                return new CommandResponse<IEnumerable<string>>(StatusCode.NotFound, "User has no roles.");

            return new CommandResponse<IEnumerable<string>>(StatusCode.Success, null, userRoles);
        }

        public async Task<CommandResponse<string>> RemoveRoleFromUserAsync(string userId, string roleName)
        {
            var user = await _db.Users.FindAsync(userId);

            if (user == null)
                return new CommandResponse<string>(StatusCode.NotFound, "User not found");

            if (!_db.Roles.Any(x => x.Name == roleName))
                return new CommandResponse<string>(StatusCode.NotFound, "Role '" + roleName + "' does not exist");

            var mappedUser = _mapper.Map<User>(user);

            await _userManager.RemoveFromRoleAsync(mappedUser, roleName);

            return new CommandResponse<string>(StatusCode.Success);
        }
    }
}
