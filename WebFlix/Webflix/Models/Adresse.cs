using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Webflix.Models;


[Table("Adresse")]
public class Adresse
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("adresseId")]
    public int AdresseId { get; set; }

    [Column("rue")] [StringLength(255)] public string Rue { get; set; }

    [Column("ville")] [StringLength(255)] public string Ville { get; set; }

    [Column("province")]
    [StringLength(255)]
    public string Province { get; set; }

    [Column("codePostal")]
    [StringLength(7)]
    public string CodePostal { get; set; }
    
    public virtual ICollection<Client> Clients { get; set; }
    public virtual ICollection<Employe> Employes { get; set; }
}
