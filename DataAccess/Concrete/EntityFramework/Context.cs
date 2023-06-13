using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entites.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Concrete.EntityFramework
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=89.252.180.91\MSSQLSERVER2016;Database=bariskar_Test;User Id=bariskar_karabe1a;Password=Kaka1034.;TrustServerCertificate=true;");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=CriminalProject;Trusted_Connection=true;Trust Server Certificate=true");
        }
        public DbSet<Criminal> Criminals { get; set; }
        public DbSet<Image> CriminalImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<ExcelCriminal> ExcelCriminals { get; set; }
    }
}

