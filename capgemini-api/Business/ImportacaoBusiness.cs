using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using capgemini_api.Business.Base;
using capgemini_api.Data;
using capgemini_api.Models.Classes;
using capgemini_api.Models.DTO;
using capgemini_api.Models.Entitys;
using capgemini_api.Models.Enum;
using ExcelDataReader;

namespace capgemini_api.Business
{
  public class ImportacaoBusiness : IBusiness<Importacao>
  {
    private readonly IMapper _mapper;
    
    private readonly ImportacaoData _data;

    public ImportacaoBusiness(ImportacaoData data)
    {
      _data = data;
    }

    public async Task Upload(byte[] fileByteArray, string fileName)
    {
      System.IO.File.WriteAllBytes(fileName, fileByteArray);

      var importacoesRaw = GetImportacoesRawFromFile(fileName);

      System.IO.File.Delete(fileName);

      var importacoes = ValidateAndObtainImportacoes(importacoesRaw);

      await AddRange(importacoes);
    }

    public async Task AddRange(List<Importacao> importacoes)
    {
      await _data.AddRange(importacoes);
    }

    public Task<List<Importacao>> Get()
    {
      return _data.Get();
    }

    public Task<Importacao> Get(long id)
    {
      return _data.Get(id);
    }

    public List<ImportacaoDTO> GetImportacoesRawFromFile(string fileName)
    {
      var importacoes = new List<ImportacaoDTO>();

      System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

      using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
      {
        using (var reader = ExcelReaderFactory.CreateReader(stream))
        {
          while (reader.Read())
          {
              var importacao = new ImportacaoDTO();

              if (reader.GetValue(0) != null)
              {
                importacao.DataEntrega = reader.GetValue(0).ToString();
              }

              if (reader.GetValue(1) != null)
              {
                importacao.NomeProduto = reader.GetValue(1).ToString();
              }

              if (reader.GetValue(2) != null)
              {
                importacao.Quantidade = reader.GetValue(2).ToString();  
              }

              if (reader.GetValue(3) != null)
              {
                importacao.ValorUnitario = reader.GetValue(3).ToString();
              }

              importacoes.Add(importacao);
          }
        }
      }

      return importacoes;
    }

    public List<Importacao> ValidateAndObtainImportacoes(List<ImportacaoDTO> importacoesRaw)
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
        throw new BusinessException(erros);
      }

      importacoes = importacoesRaw.Select(i => _mapper.Map<Importacao>(i)).ToList();

      return importacoes;
    }
    
    public List<ErrorMessage> ValidaCamposObrigatorios(ImportacaoDTO importacaoRaw, int linha)
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

    public List<ErrorMessage> ValidaIntegridadeCampos(ImportacaoDTO importacaoRaw, int linha)
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