using AutoMapper;
using capgemini_api.Application.Models.DTO;
using capgemini_api.Application.Models.ViewModel;
using capgemini_api.Domain.Models.Entitys;
using capgemini_api.Domain.Services;
using capgemini_api.Infra.Data.Services.Base;
using ExcelDataReader;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace capgemini_api.Application.Services
{
  public class ImportacaoAppService : BaseApp<Importacao, ImportacaoViewModel>
  {
    private readonly IMapper _mapper;
    
    private ImportacaoDomainService _importacaoDomainService;

    public ImportacaoAppService(ImportacaoDomainService importacaoService, IMapper mapper) : base(importacaoService, mapper)
    {
      _mapper = mapper;
      _importacaoDomainService = importacaoService;
    }

    public async Task Upload(byte[] fileByteArray, string fileName)
    {
      System.IO.File.WriteAllBytes(fileName, fileByteArray);

      var importacoesRaw = GetImportacoesRawFromFile(fileName);

      System.IO.File.Delete(fileName);

      await _importacaoDomainService.Upload(importacoesRaw);
    }

    public List<ImportacaoRawDTO> GetImportacoesRawFromFile(string fileName)
    {
      var importacoes = new List<ImportacaoRawDTO>();

      System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

      using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
      {
        using (var reader = ExcelReaderFactory.CreateReader(stream))
        {
          while (reader.Read())
          {
              var importacao = new ImportacaoRawDTO();

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
  }
}
