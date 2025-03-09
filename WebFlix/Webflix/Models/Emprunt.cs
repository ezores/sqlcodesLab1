using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models
{
    [Table("EMPRUNT")]
    public class Emprunt
    {
        [Key]
        [Column("COPIEID")]
        public int CopieId { get; set; }
        
        [Key]
        [Column("CLIENTID")]
        public int ClientId { get; set; }
        
        [Required]
        [Column("DATEDEBUTEMPRUNT")]
        public DateTime DateDebutEmprunt { get; set; }
        
        // Navigation property
        [ForeignKey(nameof(CopieId))]
        public virtual CopieFilm Copie { get; set; }
        
        // Navigation property
        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; }
    }
}