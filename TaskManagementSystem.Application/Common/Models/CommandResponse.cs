using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Enums;

namespace TaskManagementSystem.Application.Common.Models
{
    public class CommandResponse<TData>
    {
        public StatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public TData? Data { get; set; }

        public CommandResponse(StatusCode statusCode, string? message = null, TData? data = default)
        {
            StatusCode= statusCode;
            Message= message;
            Data = data;
        }
    }
}
