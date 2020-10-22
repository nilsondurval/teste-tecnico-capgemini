using System;
using System.Linq;
using System.Threading.Tasks;
using capgemini_api.Business;
using capgemini_api.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace capgemini_api.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class ImportacoesController : Controller
  {
    private readonly ImportacaoBusiness _business;

    private readonly IMultipartFormDataService _multipartFormDataService;

    public ImportacoesController(ImportacaoBusiness business, IMultipartFormDataService multipartFormDataService)
    {
      _business = business;
      _multipartFormDataService = multipartFormDataService;
    }

    [HttpGet("Teste")]
    public IActionResult Teste()
    {
      return Ok("Teste");
    }

    // POST api/importacoes/upload
    [HttpPost]
    [Route("Upload")]
    public async Task<IActionResult> Upload([FromServices] IWebHostEnvironment webHostEnvironment)
    {
      try
      {
        var files = await _multipartFormDataService.ReadMultipartFormDataAsync(Request);

        if (files == null || !files.Any())
        {
          return BadRequest("Nenhum arquivo para fazer upload");
        }

        var file = files.FirstOrDefault();
        var fileByteArray = file.Content;
        var fileName = $"{webHostEnvironment.WebRootPath}\\files\\{file.Name}";

        await _business.Upload(fileByteArray, $"{webHostEnvironment.WebRootPath}\\files\\importacoes.xlsx");

        return Ok("Importações persistidas com sucesso.");
      }
      catch (BusinessException ex)
      {
        return BadRequest(new { errors = ex.errors });
      }
      catch (Exception ex)
      {
        return BadRequest("Ocorreu um erro ao persisitir importações.");
      }
    }

    // GET api/importacoes
    [HttpGet]
    public async Task<ActionResult> Get()
    {
      try
      {
        return Ok(await _business.Get());
      }
      catch (BusinessException ex)
      {
        return BadRequest(new { errors = ex.errors });
      }
      catch (Exception)
      {
        return Ok("Ocorreu um erro ao listar importações.");
      }
    }

    // GET api/importacoes/1
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
      try
      {
        var importacao = await _business.Get(id);
        if (importacao == null)
        {
          return NotFound("Registro não encontrado");
        }

        return Ok(importacao);
      }
      catch (BusinessException ex)
      {
        return BadRequest(new { errors = ex.errors });
      }
      catch (Exception)
      {
        return Ok("Ocorreu um erro ao recuperar importação.");
      }
    }
  }
}
