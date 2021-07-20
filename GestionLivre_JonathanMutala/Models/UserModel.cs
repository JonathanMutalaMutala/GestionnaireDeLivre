using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations; 



namespace GestionLivre_JonathanMutala.Models
{
    [Table("UserTab")]
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
