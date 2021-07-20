using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionLivre_JonathanMutala.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionLivre_JonathanMutala.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {

        }
        public DbSet<UserModel> UserTable { get; set; }
    }
}
