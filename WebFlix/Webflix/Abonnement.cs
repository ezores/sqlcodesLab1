using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webflix;

[Table("ABONNEMENT")]
public class Abonnement
{
    [Key]
    [Column("CODE")]
    public string Code { get; set; }

    [Column("FORFAIT")]
    public string Forfait { get; set; }

    [Column("COUTMENSUEL")]
    public float CoutMensuel { get; set; }

    [Column("EMPRUNTMAX")]
    public int EmpruntMax { get; set; }

    [Column("DUREEMAX")]
    public int DureeMax { get; set; }
}