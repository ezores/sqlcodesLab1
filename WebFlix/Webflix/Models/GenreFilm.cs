using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models
{
    [Table("GenreFilm")]
    public class GenreFilm
    {
        [Key]
        [Column("genre")]
        [StringLength(50)]
        public string Genre { get; set; }
        
        [Key]
        [Column("filmId")]
        public int FilmId { get; set; }
        
        // Navigation property
        [ForeignKey("FilmId")]
        public virtual Film Film { get; set; }
    }
}