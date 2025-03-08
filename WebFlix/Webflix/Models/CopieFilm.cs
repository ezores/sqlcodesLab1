using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Webflix.Models
{
    // Définition de l'enum pour les statuts possibles
    public enum StatutCopie
    {
        DISPONIBLE,
        PRETE
    }
    
    [Table("COPIEFILM")]
    public class CopieFilm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("COPIEID")]
        public int CopieId { get; set; }
        
        [Required]
        [Column("FILMID")]
        public int FilmId { get; set; }
        
        [Required]
        [Column("STATUT")]
        public StatutCopie Statut { get; set; }
        
        [ForeignKey(nameof(FilmId))]
        public virtual Film Film { get; set; }
        
        public virtual Emprunt Emprunt { get; set; }
    }
}