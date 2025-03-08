using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models.Entities
{
    [Table("PAYSFILM")]
    public class PaysFilm
    {
        [Key]
        [Column("PAYSID")]
        public int PaysId { get; set; }
        
        [Key]
        [Column("FILMID")]
        public int FilmId { get; set; }
        
        // Navigation properties
        [ForeignKey("PAYSID")]
        public virtual Pays Pays { get; set; }
        
        [ForeignKey("FILMID")]
        public virtual Film Film { get; set; }
    }
}