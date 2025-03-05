using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models
{
    [Table("SCENARISTEFILM")]
    public class ScenaristeFilm
    {
        [Key]
        [Column("SCENARISTEID")]
        public int ScenaristeId { get; set; }
        
        [Key]
        [Column("FILMID")]
        public int FilmId { get; set; }
        
        // Navigation properties
        [ForeignKey("SCENARISTEID")]
        public virtual Scenariste Scenariste { get; set; }
        
        [ForeignKey("FILMID")]
        public virtual Film Film { get; set; }
    }
}