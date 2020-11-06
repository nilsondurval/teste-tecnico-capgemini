using System;

namespace capgemini_api.Application.Models.ViewModel
{
  public class ImportacaoViewModel
  {
    public long Id { get; set; }

    public DateTime DataImportacao { get; set; }

    public DateTime DataEntrega { get; set; }

    public string NomeProduto { get; set; }

    public int Quantidade { get; set; }

    public decimal ValorUnitario { get; set; }
  }
}