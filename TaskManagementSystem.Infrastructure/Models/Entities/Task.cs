using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Infrastructure.Models.Entities
{
    public class Task
    {
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? AdditionalDescription { get; set; }
        public string AssignedTo { get; set; }
        public string? AttachmentFileNames { get; set; }
    }
}
