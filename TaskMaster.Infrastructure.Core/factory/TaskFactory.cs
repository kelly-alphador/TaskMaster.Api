using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TaskMaster.Infrastructure.Core.Context;

namespace TaskMaster.Infrastructure.Core.factory
{
    public class TaskFactory:IDesignTimeDbContextFactory<TaskContext>
    {
        public TaskContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<TaskContext>();
            options.UseSqlServer("Server=DESKTOP-1PCHEEU\\SQLEXPRESS;Database=TaskMaster;Trusted_Connection=True;TrustServerCertificate=True");
            return new TaskContext(options.Options);
        }
    }
}
