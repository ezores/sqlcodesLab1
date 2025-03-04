using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Webflix.Models
{
    [Table("Film")]
    public class Film
    {
        [Key]
        [Column("filmId")]
        public int FilmId { get; set; }
        
        [Column("titre")]
        [StringLength(255)]
        public string Titre { get; set; }
        
        [Column("anneeSortie")]
        public int? AnneeSortie { get; set; }
        
        [Column("langueOriginale")]
        [StringLength(50)]
        public string LangueOriginale { get; set; }
        
        [Column("dureeMinute")]
        public int? DureeMinute { get; set; }
        
        [Column("resume")]
        [StringLength(2083)]
        public string Resume { get; set; }
        
        [Column("afficheUrl")]
        [StringLength(2083)]
        public string AfficheUrl { get; set; }
        
        [Column("realisateurId")]
        public int? RealisateurId { get; set; }
        
        // Navigation properties
        [ForeignKey("RealisateurId")]
        public virtual Personne Realisateur { get; set; }
        
        // Collections pour les relations many-to-many
        public virtual ICollection<BandeAnnonce> BandesAnnonces { get; set; }
        public virtual ICollection<CopieFilm> Copies { get; set; }
        public virtual ICollection<ActeurFilm> ActeursFilms { get; set; }
        public virtual ICollection<ScenaristeFilm> ScenaristesFilms { get; set; }
        public virtual ICollection<PaysFilm> PaysFilms { get; set; }
        public virtual ICollection<GenreFilm> GenresFilms { get; set; }
        
        // Propriétés de navigation pour accéder directement aux entités liées
        // Ces propriétés ne sont pas mappées directement à la base de données
        [NotMapped]
        public virtual IEnumerable<Personne> Acteurs => 
            ActeursFilms?.Select(af => af.Acteur);
            
        [NotMapped]
        public virtual IEnumerable<Scenariste> Scenaristes => 
            ScenaristesFilms?.Select(sf => sf.Scenariste);
            
        [NotMapped]
        public virtual IEnumerable<Pays> Pays => 
            PaysFilms?.Select(pf => pf.Pays);
            
        [NotMapped]
        public virtual IEnumerable<string> Genres => 
            GenresFilms?.Select<GenreFilm, string>(gf => gf.Genre);
            
        public Film()
        {
            // Initialisation des collections
            BandesAnnonces = new HashSet<BandeAnnonce>();
            Copies = new HashSet<CopieFilm>();
            ActeursFilms = new HashSet<ActeurFilm>();
            ScenaristesFilms = new HashSet<ScenaristeFilm>();
            PaysFilms = new HashSet<PaysFilm>();
            GenresFilms = new HashSet<GenreFilm>();
        }
    }
}