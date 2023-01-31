using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Common.Models
{
    public class UserDto : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
