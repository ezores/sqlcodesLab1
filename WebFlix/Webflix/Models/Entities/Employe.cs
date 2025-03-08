using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models.Entities
{
    [Table("EMPLOYE")]
    public class Employe
    {
        [Key]
        [Column("MATRICULE")]
        [StringLength(7)]
        public string Matricule { get; set; }
        
        [Required]
        [Column("MOTDEPASSE")]
        [StringLength(50)]
        public string MotDePasse { get; set; }
        
        [Column("COURRIEL")]
        [StringLength(320)]
        public string Courriel { get; set; }
        
        [Column("PRENOM")]
        [StringLength(50)]
        public string Prenom { get; set; }
        
        [Column("NOM")]
        [StringLength(50)]
        public string Nom { get; set; }
        
        [Column("DATENAISSANCE")]
        public DateTime? DateNaissance { get; set; }
        
        [Column("NUMEROTELEPHONE")]
        [StringLength(11)]
        public string NumeroTelephone { get; set; }
        
        [Column("ADRESSEID")]
        public int? AdresseId { get; set; }
        
        // Navigation property
        [ForeignKey("ADRESSEID")]
        public virtual Adresse Adresse { get; set; }
    }
}