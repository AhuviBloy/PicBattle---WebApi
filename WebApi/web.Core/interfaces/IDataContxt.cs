using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using web.Core.models;
using Microsoft.EntityFrameworkCore;


namespace web.Core.interfaces
{
    public interface IDataContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Creation> Creations { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<WinCreation> WinCreations { get; set; }

    }
}
