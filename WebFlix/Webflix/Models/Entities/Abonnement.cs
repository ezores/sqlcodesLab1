using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models.Entities
{
    [Table("ABONNEMENT")]
    public class Abonnement
    {
        [Key]
        [Column("CODE")]
        [StringLength(1)]
        public string Code { get; set; }  // Use string instead of char
        
        [Column("FORFAIT")]
        [StringLength(14)]
        public string Forfait { get; set; }
        
        [Column("COUTMENSUEL")]
        public float? CoutMensuel { get; set; }
        
        [Column("EMPRUNTMAX")]
        public int? EmpruntMax { get; set; }
        
        [Column("DUREEMAX")]
        public int? DureeMax { get; set; }
        
        // Navigation property
        public virtual ICollection<Client> Clients { get; set; }
        
        public Abonnement()
        {
            // Initialisation des collections
            Clients = new HashSet<Client>();
        }
    }
}