using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models.Entities
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
        [ForeignKey(nameof(ScenaristeId))]
        public virtual Scenariste Scenariste { get; set; }
        
        [ForeignKey(nameof(FilmId))]
        public virtual Film Film { get; set; }
    }
}