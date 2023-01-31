using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Application.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManagementSystem.Infrastructure.Models.Options;
using Microsoft.Extensions.Options;
using TaskManagementSystem.Infrastructure.Helpers;
using TaskManagementSystem.Infrastructure.Models.Entities;
using Microsoft.Extensions.Logging;
using Isopoh.Cryptography.Argon2;
using TaskManagementSystem.Application.User.Commands.AuthenticateUser;
using TaskManagementSystem.Application.User.Commands.RegisterUser;
using TaskManagementSystem.Application.User.Commands.UpdateUser;

namespace TaskManagementSystem.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly AppDBContext _db;
        private readonly UserManager<User> _userManager;
        private readonly JWTAuthOptions _jwtAuthOptions;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(AppDBContext db,
                           IMapper mapper,
                           UserManager<User> userManager,
                           IOptions<JWTAuthOptions> jwtAuthOptions,
                           ILogger<UserService> logger)
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _jwtAuthOptions = jwtAuthOptions?.Value ?? throw new ArgumentNullException(nameof(jwtAuthOptions));
            _logger = logger;
        }

        public async Task<bool> AnyAsync()
        {
            return await _db.Users.AnyAsync();
        }

        public async Task<CommandResponse<AuthenticateUserResponse>> AuthenticateAsync(AuthenticateUserCommandModel model)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.UserName == model.Username);

            if (user == null || !Argon2.Verify(user.PasswordHash, model.Password))
                return new CommandResponse<AuthenticateUserResponse>(StatusCode.Unauthorized, "Username or password is incorrect");

            var userRoles = await _userManager.GetRolesAsync(user);

            string accessToken = ServiceHelper.GetAccessToken(model.Username, model.Password, userRoles.ToList(), _jwtAuthOptions.Secret);

            var authenticateUser = _mapper.Map<AuthenticateUserResponse>(user);

            authenticateUser.Token = accessToken;
            
            return new CommandResponse<AuthenticateUserResponse>(StatusCode.Success, null, authenticateUser);
        }

        public async Task<CommandResponse<string>> DeleteAsync(string id)
        {
            var user = await _db.Users.FindAsync(id);

            if (user == null)
                return new CommandResponse<string>(StatusCode.NotFound, "User could not be found.", id);

            _db.Users.Remove(user);
            
            await _db.SaveChangesAsync();

            return new CommandResponse<string>(StatusCode.Success, "Deleted");
        }

        public async Task<CommandResponse<IEnumerable<UserDto>>> GetAllAsync()
        {
            var users = await _db.Users.ToListAsync();

            var mappedUsers = _mapper.Map<IEnumerable<UserDto>>(users);

            return new CommandResponse<IEnumerable<UserDto>>(StatusCode.Success, null, mappedUsers);
        }

        public async Task<CommandResponse<UserDto>> GetByIdAsync(string id)
        {
            var user = await _db.Users.FindAsync(id);

            if (user == null)
                return new CommandResponse<UserDto>(StatusCode.NotFound, "User could not be found.");

            var userDto = _mapper.Map<UserDto>(user);

            return new CommandResponse<UserDto>(StatusCode.Success, null, userDto);
        }

        public async Task<CommandResponse<UserDto>> RegisterAsync(RegisterUserCommandModel model)
        {
            if (_db.Users.Any(x => x.UserName == model.UserName))
                return new CommandResponse<UserDto>(StatusCode.Conflict, "Username '" + model.UserName + "' is already taken");

            try
            {
                var user = _mapper.Map<User>(model);

                user.PasswordHash =  Argon2.Hash(model.Password);

                var newUser = await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                await _userManager.AddToRoleAsync(newUser.Entity, "Basic");

                var mappedUser = _mapper.Map<UserDto>(newUser.Entity);

                return new CommandResponse<UserDto>(StatusCode.Success, null, mappedUser);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CommandResponse<UserDto>(StatusCode.GenericError, ex.Message);
            }
        }

        public async Task<CommandResponse<UserDto>> UpdateAsync(string id, UpdateUserCommandModel model)
        {
            try
            {
                var user = await _db.Users.FindAsync(id);

                if (user == null)
                    return new CommandResponse<UserDto>(StatusCode.NotFound, "User could not be found.");

                if (_db.Users.Any(x => x.UserName == model.UserName && x.Id != id))
                    return new CommandResponse<UserDto>(StatusCode.Conflict, "Username '" + model.UserName + "' is already taken");

                user.Id = id;
                user.PasswordHash = Argon2.Hash(model.Password);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.UserName;

                var updatedUser = _db.Users.Update(user);
                await _db.SaveChangesAsync();

                var mappedUser = _mapper.Map<UserDto>(updatedUser.Entity);

                return new CommandResponse<UserDto>(StatusCode.Success, null, mappedUser);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CommandResponse<UserDto>(StatusCode.GenericError, ex.Message);
            }
        }
    }
}
