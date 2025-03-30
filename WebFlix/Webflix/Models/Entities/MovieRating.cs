using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Webflix.Models.Entities;

[Keyless]
[Table("VUE_MOYENNE")]
public class MovieRating
{
    [Column("IDFILM")]
    public int FilmId { get; set; }
    
    [Column("MOYENNE_COTE")]
    public double Score { get; set; }
}