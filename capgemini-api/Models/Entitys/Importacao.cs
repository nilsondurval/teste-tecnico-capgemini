using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace capgemini_api.Models.Entitys
{
  [Table("importacao")]
  public class Importacao
  {
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("dataImportacao")]
    [Required]
    public DateTime DataImportacao { get; set; }

    [Column("dataEntrega")]
    [Required]
    public DateTime DataEntrega { get; set; }

    [Column("nomeProduto")]
    [Required]
    [MaxLength(50)]
    public string NomeProduto { get; set; }

    [Column("quantidade")]
    [Required]
    public int Quantidade { get; set; }

    [Column("valorUnitario")]
    [Required]
    public decimal ValorUnitario { get; set; }
  }
}