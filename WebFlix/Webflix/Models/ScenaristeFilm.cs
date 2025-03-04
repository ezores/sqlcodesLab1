using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models
{
    [Table("ScenaristeFilm")]
    public class ScenaristeFilm
    {
        [Key]
        [Column("scenaristeId")]
        public int ScenaristeId { get; set; }
        
        [Key]
        [Column("filmId")]
        public int FilmId { get; set; }
        
        // Navigation properties
        [ForeignKey("ScenaristeId")]
        public virtual Scenariste Scenariste { get; set; }
        
        [ForeignKey("FilmId")]
        public virtual Film Film { get; set; }
    }
}