using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix.Models.Entities;

[Table("CLIENTMOVIE")]
public class ClientMovie
{
    [Key]
    [Column("CLIENTID")]
    public int ClientId { get; set; }
        
    [Key]
    [Column("MOVIEID")]
    public int MovieId { get; set; }
        
    // Navigation properties
    [ForeignKey(nameof(ClientId))]
    public virtual Client Client { get; set; }
        
    [ForeignKey(nameof(MovieId))]
    public virtual Film Film { get; set; }
}