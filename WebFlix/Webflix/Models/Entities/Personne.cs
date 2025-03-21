using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Webflix.Models.Entities
{
    [Table("PERSONNE")]
    public class Personne
    {
        [Key]
        [Column("PERSONNEID")]
        public int? PersonneId { get; set; }
        
        [Column("NOM")]
        [StringLength(100)]
        public string? Nom { get; set; }
        
        [Column("DATENAISSANCE")]
        public DateTime? DateNaissance { get; set; }
        
        [Column("LIEUNAISSANCE")]
        [StringLength(100)]
        public string? LieuNaissance { get; set; }
        
        [Column("PHOTO")]
        [StringLength(2048)]
        public string? Photo { get; set; }
        
        [Column("BIOGRAPHIE")]
        public string? Biographie { get; set; }
        
        // Navigation properties pour les relations
        
        // Films où cette personne est acteur
        public virtual ICollection<ActeurFilm> FilmsCommeActeur { get; set; }
        
        // Films réalisés par cette personne
        public virtual ICollection<Film> FilmsRealises { get; set; }
        
        // Propriété calculée pour accéder facilement aux films où cette personne joue
        [NotMapped]
        public virtual IEnumerable<Film> FilmsJoues => 
            FilmsCommeActeur?.Select(af => af.Film);
            
        // Propriété calculée pour accéder aux personnages joués par cette personne
        [NotMapped]
        public virtual IDictionary<Film, string> PersonnagesJoues => 
            FilmsCommeActeur?.ToDictionary(
                af => af.Film,
                af => af.Personnage
            );
        
        public Personne()
        {
            // Initialisation des collections
            FilmsCommeActeur = new HashSet<ActeurFilm>();
            FilmsRealises = new HashSet<Film>();
        }
    }
}