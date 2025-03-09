using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models.Entities;

[Table("BANDEANNONCE")]
public class BandeAnnonce
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("BANDEANNONCEID")]
    public int Id { get; set; }
    
    [Required]
    [Column("FILMID")]
    public int FilmId { get; set; }
    
    [Column("URL")]
    [StringLength(2048)]
    public string Url { get; set; }
    
    [ForeignKey(nameof(FilmId))]
    public virtual Film Film { get; set; }
}
