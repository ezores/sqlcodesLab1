using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("CARTECREDIT")]
public class CarteCredit
{
    [Key]
    [Column("CARTECREDITID")]
    public int Id { get; set; }

    [Column("NUMERO")]
    public string Numero { get; set; }

    [Column("DATEEXPIRATION")]
    public DateTime DateExpiration { get; set; }

    [Column("CVV")]
    public string CVV { get; set; }

    [Column("TYPECARTE")]
    public string TypeCarte { get; set; }
}