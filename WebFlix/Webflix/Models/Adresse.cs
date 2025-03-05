using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Webflix.Models;


[Table("ADRESSE")]
public class Adresse
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ADRESSEID")]
    public int AdresseId { get; set; }

    [Column("RUE")] [StringLength(255)] public string Rue { get; set; }

    [Column("VILLE")] [StringLength(255)] public string Ville { get; set; }

    [Column("PROVINCE")]
    [StringLength(255)]
    public string Province { get; set; }

    [Column("CODEPOSTAL")]
    [StringLength(7)]
    public string CodePostal { get; set; }
    
    public virtual ICollection<Client> Clients { get; set; }
    public virtual ICollection<Employe> Employes { get; set; }
}
