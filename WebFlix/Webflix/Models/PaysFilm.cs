using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models
{
    [Table("PaysFilm")]
    public class PaysFilm
    {
        [Key]
        [Column("paysId")]
        public int PaysId { get; set; }
        
        [Key]
        [Column("filmId")]
        public int FilmId { get; set; }
        
        // Navigation properties
        [ForeignKey("PaysId")]
        public virtual Pays Pays { get; set; }
        
        [ForeignKey("FilmId")]
        public virtual Film Film { get; set; }
    }
}