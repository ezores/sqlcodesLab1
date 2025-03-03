using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Webflix;

[Table("EMPRUNT")]
public class Emprunt
{
    [Key]
    [Column("COPIEID")]
    public int CopieId { get; set; }

    [Column("NOMUSAGER")]
    public string NomUsager { get; set; }

    [Column("DATEDEBUTEMPRUNT")] public DateTime DateDebutEmprunt { get; set; }
}