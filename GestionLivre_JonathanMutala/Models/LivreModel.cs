using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionLivre_JonathanMutala.Models
{
    public class LivreModel
    {
        [Key]
        public int IDLivre { get; set; }
        [Required]
        public string TitreLivre { get; set; }
        [Required]
        public string AuteurLivre { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatePublicationLivre { get; set; }
        
        public string ImagePath { get; set; }
        public IFormFile imageFile { get; set; }

    }
}
