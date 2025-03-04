using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models
{
    [Table("Client")]
    public class Client
    {
        [Key]
        [Column("clientId")]
        public int ClientId { get; set; }
        
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
        [StringLength(13)]
        public string NumeroTelephone { get; set; }
        
        [Column("adresseId")]
        public int? AdresseId { get; set; }
        
        [Column("carteCreditId")]
        [StringLength(16)]
        public string CarteCreditId { get; set; }
        
        [Column("codeAbonnement")]
        [StringLength(1)]
        public char? CodeAbonnement { get; set; }
        
        // Navigation properties
        [ForeignKey("AdresseId")]
        public virtual Adresse Adresse { get; set; }
        
        [ForeignKey("CarteCreditId")]
        public virtual CarteCredit CarteCredit { get; set; }
        
        [ForeignKey("CodeAbonnement")]
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