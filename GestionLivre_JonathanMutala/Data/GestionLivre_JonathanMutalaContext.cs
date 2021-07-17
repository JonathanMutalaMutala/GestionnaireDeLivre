using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestionLivre_JonathanMutala.Models;

namespace GestionLivre_JonathanMutala.Data
{
    public class GestionLivre_JonathanMutalaContext : DbContext
    {
        public GestionLivre_JonathanMutalaContext (DbContextOptions<GestionLivre_JonathanMutalaContext> options)
            : base(options)
        {
        }

        public DbSet<GestionLivre_JonathanMutala.Models.LivreModel> LivreModel { get; set; }
    }
}
