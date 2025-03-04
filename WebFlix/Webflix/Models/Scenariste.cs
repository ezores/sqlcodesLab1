using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Webflix.Models
{
    [Table("Scenariste")]
    public class Scenariste
    {
        [Key]
        [Column("scenaristeId")]
        public int ScenaristeId { get; set; }
        
        [Column("nom")]
        [StringLength(50)]
        public string Nom { get; set; }
        
        // Navigation properties pour les relations
        public virtual ICollection<ScenaristeFilm> ScenaristesFilms { get; set; }
        
        // Propriété calculée pour accéder facilement aux films écrits par ce scénariste
        [NotMapped]
        public virtual IEnumerable<Film> Films => 
            ScenaristesFilms?.Select(sf => sf.Film);
        
        public Scenariste()
        {
            // Initialisation des collections
            ScenaristesFilms = new HashSet<ScenaristeFilm>();
        }
    }
}