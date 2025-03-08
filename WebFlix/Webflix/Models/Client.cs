using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models
{
    [Table("CLIENT")]
    public class Client
    {
        [Key]
        [Column("CLIENTID")]
        public int ClientId { get; set; }
        
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
        [StringLength(13)]
        public string NumeroTelephone { get; set; }
        
        [Column("ADRESSEID")]
        public int? AdresseId { get; set; }
        
        [Column("CARTECREDITID")]
        //[StringLength(16)]
        public int? CarteCreditId { get; set; }
        
        [Column("CODEABONNEMENT")]
        [StringLength(1)]
        public string CodeAbonnement { get; set; }
        
        // Navigation properties
        [ForeignKey(nameof(AdresseId))]
        public virtual Adresse Adresse { get; set; }
        
        [ForeignKey(nameof(CarteCreditId))]
        public virtual CarteCredit CarteCredit { get; set; }
        
        [ForeignKey(nameof(CodeAbonnement))]
        public virtual Abonnement Abonnement { get; set; }
        
        // Collection pour les emprunts
        public virtual ICollection<Emprunt> Emprunts { get; set; }
        
        public Client()
        {
            // Initialisation des collections
            Emprunts = new HashSet<Emprunt>();
        }
    }
}