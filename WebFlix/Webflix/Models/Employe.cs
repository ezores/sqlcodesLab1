using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models
{
    [Table("Employe")]
    public class Employe
    {
        [Key]
        [Column("matricule")]
        [StringLength(7)]
        public string Matricule { get; set; }
        
        [Required]
        [Column("motDePasse")]
        [StringLength(50)]
        public string MotDePasse { get; set; }
        
        [Column("courriel")]
        [StringLength(320)]
        public string Courriel { get; set; }
        
        [Column("prenom")]
        [StringLength(50)]
        public string Prenom { get; set; }
        
        [Column("nom")]
        [StringLength(50)]
        public string Nom { get; set; }
        
        [Column("dateNaissance")]
        public DateTime? DateNaissance { get; set; }
        
        [Column("numeroTelephone")]
        [StringLength(11)]
        public string NumeroTelephone { get; set; }
        
        [Column("adresseId")]
        public int? AdresseId { get; set; }
        
        // Navigation property
        [ForeignKey("AdresseId")]
        public virtual Adresse Adresse { get; set; }
    }
}