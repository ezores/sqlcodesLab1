using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Webflix.Models.Entities
{
    [Table("PAYS")]
    public class Pays
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PAYSID")]
        public int? PaysId { get; set; }
        
        [Column("NOM")]
        [StringLength(255)]
        public string? Nom { get; set; }
        
        // Navigation properties pour les relations
        public virtual ICollection<PaysFilm> PaysFilms { get; set; }
        
        // Propriété calculée pour accéder facilement aux films associés à ce pays
        [NotMapped]
        public virtual IEnumerable<Film> Films => 
            PaysFilms?.Select(pf => pf.Film);
        
        public Pays()
        {
            // Initialisation des collections
            PaysFilms = new HashSet<PaysFilm>();
        }
    }
}