using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceExample.Models
{
    public class FileContext : DbContext
    {
        public DbSet<File> Files { get; set; }

        public FileContext()
        {
            Database.EnsureCreated();
        }
        public FileContext(DbContextOptions<FileContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=FileSystem;Trusted_Connection=True;");
        }
    }
}
