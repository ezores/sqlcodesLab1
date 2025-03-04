using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models;

[Table("ActeurFilm")]
public class ActeurFilm
{
    [Key]
    [Column("acteurId")]
    public int ActeurId { get; set; }
    
    [Key]
    [Column("filmId")]
    public int FilmId { get; set; }
    
    [Column("personnage")]
    [StringLength(100)]
    public string Personnage { get; set; }
    
    [ForeignKey("ActeurId")]
    public virtual Personne Acteur { get; set; }
    
    [ForeignKey("FilmId")]
    public virtual Film Film { get; set; }
}