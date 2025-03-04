using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models;

[Table("BandeAnnonce")]
public class BandeAnnonce
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("bandeAnnonceId")]
    public int Id { get; set; }
    
    [Required]
    [Column("filmId")]
    public int FilmId { get; set; }
    
    [Column("URL")]
    [StringLength(2048)]
    public string Url { get; set; }
    
    [ForeignKey("FilmId")]
    public virtual Film Film { get; set; }
}
