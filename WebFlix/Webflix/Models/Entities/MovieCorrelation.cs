using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Webflix.Models.Entities;

[Keyless]
[Table("VUE_CORRELATIONS")]
public class MovieCorrelation
{
    [Column("FILM_J")]
    public int FirstMovie { get; set; }
    
    [Column("FILM_K")]
    public int SecondMovie { get; set; }
    
    [Column("CORRELATION")]
    public double Correlation { get; set; }
}