using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using capgemini_api.Application.Models.DTO;
using capgemini_api.Domain.Base;
using capgemini_api.Domain.Exceptions;
using capgemini_api.Domain.Models.Classes;
using capgemini_api.Domain.Models.Entitys;
using capgemini_api.Domain.Models.Enum;
using capgemini_api.Infra.Data.Repository;

namespace capgemini_api.Domain.Services
{
  public class ImportacaoDomainService : BaseDomain<Importacao>
  {
    private readonly IMapper _mapper;
    
    private readonly ImportacaoRepository _repository;

    public ImportacaoDomainService(ImportacaoRepository repository, IMapper mapper) : base(repository)
    {
      _mapper = mapper;
      _repository = repository;
    }

    public async Task Upload(List<ImportacaoRawDTO> importacoesRaw)
    {
      var importacoes = ValidateAndObtainImportacoes(importacoesRaw);

      await AddRangeAsync(importacoes);
    }

    public List<Importacao> ValidateAndObtainImportacoes(List<ImportacaoRawDTO> importacoesRaw)
    {
      var importacoes = new List<Importacao>();
      var erros = new List<ErrorMessage>();
      var count = 1;

      foreach (var importacaoRaw in importacoesRaw)
      {
        erros.AddRange(ValidaCamposObrigatorios(importacaoRaw, count));
        erros.AddRange(ValidaIntegridadeCampos(importacaoRaw, count));

        count++;
      }

      if (erros.Any())
      {
        throw new DomainException(erros);
      }

      importacoes = importacoesRaw.Select(i => _mapper.Map<Importacao>(i)).ToList();

      return importacoes;
    }
    
    public List<ErrorMessage> ValidaCamposObrigatorios(ImportacaoRawDTO importacaoRaw, int linha)
    {
      var erros = new List<ErrorMessage>();

      if (string.IsNullOrWhiteSpace(importacaoRaw.DataEntrega))
      {
        erros.Add(new ErrorMessage()
        {
          Codigo = ErrorEnum.ERROR,
          Message = $"Erro na linha {linha}, Campo 'Data Entrega' obrigatório"
        });
      }

      if (string.IsNullOrWhiteSpace(importacaoRaw.NomeProduto))
      {
        erros.Add(new ErrorMessage()
        {
          Codigo = ErrorEnum.ERROR,
          Message = $"Erro na linha {linha}, Campo 'Nome do Produto' obrigatório"
        });
      }

      if (string.IsNullOrWhiteSpace(importacaoRaw.Quantidade))
      {
        erros.Add(new ErrorMessage()
        {
          Codigo = ErrorEnum.ERROR,
          Message = $"Erro na linha {linha}, Campo 'Quantidade' obrigatório"
        });
      }

      if (string.IsNullOrWhiteSpace(importacaoRaw.ValorUnitario))
      {
        erros.Add(new ErrorMessage()
        {
          Codigo = ErrorEnum.ERROR,
          Message = $"Erro na linha {linha}, Campo 'Valor Unitário' obrigatório"
        });
      }

      return erros;
    }

    public List<ErrorMessage> ValidaIntegridadeCampos(ImportacaoRawDTO importacaoRaw, int linha)
    {
      var erros = new List<ErrorMessage>();
      DateTime dataEntrega;
      int quantidade;
      decimal valorUnitario;

      if (!DateTime.TryParse(importacaoRaw.DataEntrega, out dataEntrega))
      {
        erros.Add(new ErrorMessage()
        {
          Codigo = ErrorEnum.ERROR,
          Message = $"Erro na linha {linha}, Campo 'Data Entrega' inválido"
        });
      }

      if (dataEntrega <= DateTime.Now.Date)
      {
        erros.Add(new ErrorMessage()
        {
          Codigo = ErrorEnum.ERROR,
          Message = $"Erro na linha {linha}, Campo 'Data Entrega' deve ser maior que a data atual"
        });
      }

      if (!string.IsNullOrWhiteSpace(importacaoRaw.NomeProduto) && importacaoRaw.NomeProduto.Length > 50)
      {
        erros.Add(new ErrorMessage()
        {
          Codigo = ErrorEnum.ERROR,
          Message = $"Erro na linha {linha}, Campo 'Nome do Produto' deve ser de no máximo 50 caracteres"
        });
      }

      if (!int.TryParse(importacaoRaw.Quantidade, out quantidade))
      {
        erros.Add(new ErrorMessage()
        {
          Codigo = ErrorEnum.ERROR,
          Message = $"Erro na linha {linha}, Campo 'Quantidade' inválido"
        });
      }

      if (quantidade <= 0)
      {
        erros.Add(new ErrorMessage()
        {
          Codigo = ErrorEnum.ERROR,
          Message = $"Erro na linha {linha}, Campo 'Quantidade' deve ser maior que zero"
        });
      }

      if (!decimal.TryParse(importacaoRaw.ValorUnitario, out valorUnitario))
      {
        erros.Add(new ErrorMessage()
        {
          Codigo = ErrorEnum.ERROR,
          Message = $"Erro na linha {linha}, Campo 'Valor Unitário' inválido"
        });
      }

      if (valorUnitario <= 0)
      {
        erros.Add(new ErrorMessage()
        {
          Codigo = ErrorEnum.ERROR,
          Message = $"Erro na linha {linha}, Campo 'Valor Unitário' deve ser maior que zero"
        });
      }

      return erros;
    }
  }
}