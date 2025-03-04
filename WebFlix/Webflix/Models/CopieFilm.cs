using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Webflix.Models
{
    // Définition de l'enum pour les statuts possibles
    public enum StatutCopie
    {
        Disponible,
        Prete
    }
    
    [Table("CopieFilm")]
    public class CopieFilm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("copieId")]
        public int CopieId { get; set; }
        
        [Required]
        [Column("filmId")]
        public string FilmId { get; set; }
        
        [Required]
        [Column("statut")]
        public StatutCopie Statut { get; set; }
        
        [ForeignKey("FilmId")]
        public virtual Film Film { get; set; }
        
        public virtual Emprunt Emprunt { get; set; }
    }
}