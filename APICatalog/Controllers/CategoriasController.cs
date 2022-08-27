using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace APICatalog.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly APICatalogContext _context;
    public CategoriasController(APICatalogContext context)
    {
        _context = context;
    }

    [HttpGet("produtos")]
    //Visibilidade - tipo de retorno - nome do método 
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos(string nomeProduto)
    {
        return await _context.Categorias
               .Include(c => c.Produtos.Where(p => p.Nome.Contains(nomeProduto)))
               .ToListAsync();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> Get()
    {
        var categoria = _context.Categorias.AsNoTracking().ToListAsync();
        if (categoria is null)
        {
            return BadRequest();
        }
        else
        {
            return await categoria;
        }
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<Categoria>> Get(int id)
    {
        var categoria = _context.Categorias.FirstOrDefaultAsync(p => p.CategoriaId == id);
        if (categoria is null)
        {
            return NotFound("Categoria não encontrada...");
        }
        else
        {
            return await categoria;
        }
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria) // Um objeto complexo é um tipo de var que tem varios tipos dentros
    {
        _context.Categorias.Add(categoria);
        _context.SaveChanges();
        if (categoria is null)
        {
            return BadRequest();
        }
        else
        {
            return Ok(categoria);
        }
    }

    [HttpPut]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }
        else
        {
            _context.UpdateRange(categoria);
            _context.SaveChanges();
            return Ok(categoria);
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categorias = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
        if (categorias is null)
        {
            return BadRequest("Categoria não encontrada...");
        }
        else
        {
            _context.Categorias.Remove(categorias);
            _context.SaveChanges();
            return Ok(categorias);
        }
    }



}
