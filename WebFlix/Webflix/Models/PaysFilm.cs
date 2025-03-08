using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Webflix.Models
{
    [Table("PAYSFILM")]
    public class PaysFilm
    {
        [Key]
        [Column("PAYSID")]
        public int? PaysId { get; set; }
        
        [Key]
        [Column("FILMID")]
        public int? FilmId { get; set; }
        
        // Navigation properties
        [ForeignKey(nameof(PaysId))]
        public virtual Pays Pays { get; set; }
        
        [ForeignKey(nameof(FilmId))]
        public virtual Film Film { get; set; }
    }
}