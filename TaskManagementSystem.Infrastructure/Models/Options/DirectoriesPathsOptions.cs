using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Infrastructure.Models.Options
{
    public class DirectoriesPathsOptions
    {
        public const string SectionName = "DirectoriesPaths";
        public string TasksFilesPath { get; set; }
    }
}
