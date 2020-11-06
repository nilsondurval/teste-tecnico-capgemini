using System;
using System.Linq;
using System.Threading.Tasks;
using Arrecadacao_core.Controllers;
using capgemini_api.Application.Services;
using capgemini_api.Application.Services.Interfaces;
using capgemini_api.Domain.Exceptions;
using capgemini_api.Domain.Models.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace capgemini_api.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class ImportacoesController : BaseController
  {
    private readonly ImportacaoAppService _appService;

    private readonly IMultipartFormDataAppService _multipartFormDataService;

    public ImportacoesController(ImportacaoAppService appService, IMultipartFormDataAppService multipartFormDataService)
    {
      _appService = appService;
      _multipartFormDataService = multipartFormDataService;
    }

    // POST api/importacoes/upload
    [HttpPost]
    [Route("Upload")]
    public async Task<IActionResult> Upload([FromServices] IWebHostEnvironment webHostEnvironment)
    {
      try
      {
        var multipartContent = await _multipartFormDataService.ReadMultipartFormDataAsync<object>(Request, "importacoes");

        if (multipartContent == null || !multipartContent.Files.Any())
        {
          return BadRequest(ErrorEnum.ERROR, "Nenhum arquivo para fazer upload");
        }

        var file = multipartContent.Files.FirstOrDefault();
        var fileByteArray = file.Content;
        var fileName = $"{webHostEnvironment.WebRootPath}\\files\\{file.Name}";

        await _appService.Upload(fileByteArray, fileName);

        return Ok("Importações persistidas com sucesso.");
      }
      catch (DomainException ex)
      {
        return BadRequest(new { errors = ex.errors });
      }
      catch (Exception ex)
      {
        return BadRequest(ErrorEnum.ERROR, "Ocorreu um erro inesperado.");
      }
    }

    // GET api/importacoes
    [HttpGet]
    public IActionResult Get()
    {
      try
      {
        return Ok(_appService.Get().ToList());
      }
      catch (DomainException ex)
      {
        return BadRequest(new { errors = ex.errors });
      }
      catch (Exception ex)
      {
        return BadRequest(ErrorEnum.ERROR, "Ocorreu um erro inesperado.");
      }
    }

    // GET api/importacoes/1
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
      try
      {
        var importacao = await _appService.GetAsync(id);
        if (importacao == null)
        {
          return NotFound(ErrorEnum.ERROR, "Registro não encontrado");
        }

        return Ok(importacao);
      }
      catch (DomainException ex)
      {
        return BadRequest(new { errors = ex.errors });
      }
      catch (Exception ex)
      {
        return BadRequest(ErrorEnum.ERROR, "Ocorreu um erro inesperado.");
      }
    }
  }
}
