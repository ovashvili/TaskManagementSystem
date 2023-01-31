using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;

namespace TaskManagementSystem.Application.User.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<CommandResponse<IEnumerable<UserDto>>>
    {

    }
}
