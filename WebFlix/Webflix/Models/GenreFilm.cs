using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models
{
    [Table("GENREFILM")]
    public class GenreFilm
    {
        [Key]
        [Column("GENRE")]
        [StringLength(50)]
        public string Genre { get; set; }
        
        [Key]
        [Column("FILMID")]
        public int FilmId { get; set; }
        
        // Navigation property
        [ForeignKey("FILMID")]
        public virtual Film Film { get; set; }
    }
}