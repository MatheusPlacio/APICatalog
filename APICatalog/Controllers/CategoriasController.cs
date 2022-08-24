using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        return _context.Categorias.Include(p => p.Produtos).ToList();
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        var categoria = _context.Categorias.ToList();
        if (categoria is null)
        {
            return BadRequest();
        }
        else
        {
            return Ok(categoria);
        }        
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
        if (categoria is null)
        {
            return NotFound("Categoria não encontrada...");
        }
        else
        {
            return Ok(categoria);
        }
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
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
