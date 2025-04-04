using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models.Entities;

[Table("ACTEURFILM")]
public class ActeurFilm
{
    [Key]
    [Column("ACTEURID")]
    public int? ActeurId { get; set; }
    
    [Key]
    [Column("FILMID")]
    public int? FilmId { get; set; }
    
    [Column("PERSONNAGE")]
    [StringLength(100)]
    public string? Personnage { get; set; }
    
    [ForeignKey(nameof(ActeurId))]
    public virtual Personne Acteur { get; set; }
    
    [ForeignKey(nameof(FilmId))]
    public virtual Film Film { get; set; }
}