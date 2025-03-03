using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Webflix;

[Table("EMPLOYE")]
public class Employe
{
    [Key]
    [Column("MATRICULE")]
    public string Matricule { get; set; }

    [Column("MOTDEPASSE")]
    public string MotDePasse { get; set; }

    [Column("COURRIEL")]
    public string Courriel { get; set; }

    [Column("PRENOM")]
    public string Prenom { get; set; }

    [Column("NOM")]
    public string Nom { get; set; }
    
    [Column("DATENAISSANCE")]
    public DateTime DateNaissance { get; set; }

    [Column("NUMEROTELEPHONE")]
    public string NumeroTelephone { get; set; }

    [Column("ADRESSEID")]
    public int AdresseId { get; set; }
}