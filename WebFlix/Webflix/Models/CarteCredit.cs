using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models
{
    [Table("CarteCredit")]
    public class CarteCredit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("carteCreditId")]
        public int CarteCreditId { get; set; }
        
        [Required]
        [Column("numero")]
        [StringLength(19)]
        public string Numero { get; set; }
        
        [Required]
        [Column("dateExpiration")]
        public DateTime DateExpiration { get; set; }
        
        [Column("CVV")]
        [StringLength(3)]
        public string CVV { get; set; }
        
        [Column("typeCarte")]
        [StringLength(50)]
        public string TypeCarte { get; set; }
        
        // Navigation property
        public virtual ICollection<Client> Clients { get; set; }
        
        public CarteCredit()
        {
            // Initialisation des collections
            Clients = new HashSet<Client>();
        }
    }
}