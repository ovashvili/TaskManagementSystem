using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Infrastructure.Models.Entities;

namespace TaskManagementSystem.Infrastructure.Helpers
{
    public class ServiceHelper
    {
        internal static string GetAccessToken(string userName, string password, List<string> userRoles, string secret)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
            };

            foreach (var role in userRoles)
                claims.Add(new Claim("Role", role));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "jwt",
                audience: "jwt-audience",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    algorithm: SecurityAlgorithms.HmacSha256
                )
             );

            return (new JwtSecurityTokenHandler()).WriteToken(token);
        }

        internal static async Task<List<string>> UploadFilesAsync(string directoryPath, List<IFormFile> attachedFiles)
        {
            var fileNames = new List<string>();
            foreach (var file in attachedFiles)
            {
                var fileName = $"{Guid.NewGuid().ToString("N")}-{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(directoryPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    file.CopyTo(stream);
                    fileNames.Add(fileName);
                }
            }

            return fileNames;
        }
    }
}
