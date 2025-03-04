using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models
{
    [Table("Abonnement")]
    public class Abonnement
    {
        [Key]
        [Column("code")]
        [StringLength(1)]
        public char Code { get; set; }
        
        [Column("forfait")]
        [StringLength(14)]
        public string Forfait { get; set; }
        
        [Column("coutMensuel")]
        public float? CoutMensuel { get; set; }
        
        [Column("empruntMax")]
        public int? EmpruntMax { get; set; }
        
        [Column("dureeMax")]
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