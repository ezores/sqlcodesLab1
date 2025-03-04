using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models
{
    [Table("Emprunt")]
    public class Emprunt
    {
        [Key]
        [Column("copieId")]
        public int CopieId { get; set; }
        
        [Key]
        [Column("nomUsager")]
        [StringLength(50)]
        public string NomUsager { get; set; }
        
        [Required]
        [Column("dateDebutEmprunt")]
        public DateTime DateDebutEmprunt { get; set; }
        
        // Navigation property
        [ForeignKey("CopieId")]
        public virtual CopieFilm Copie { get; set; }
    }
}