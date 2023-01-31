using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;

namespace TaskManagementSystem.Application.User.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<CommandResponse<UserDto>>
    {
        public string Id { get; set; }
    }
}
