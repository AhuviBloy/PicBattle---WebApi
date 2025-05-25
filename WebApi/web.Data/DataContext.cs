using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;
using web.Core.interfaces;


namespace web.Data
{
    public class DataContext:DbContext,IDataContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Creation> Creations { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<WinCreation> WinCreations { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        //דאטה בייס מקומי
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=PicBattle");
        //}
    }
}
