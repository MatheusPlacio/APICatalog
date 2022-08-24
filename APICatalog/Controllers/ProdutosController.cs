using APICatalog.Context;
using APICatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly APICatalogContext _context; // Injeção de independencia 

    public ProdutosController(APICatalogContext context) // construtor
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<Produto>> Get()
    {
        var produtos = _context.Produtos.ToList();
        if(produtos is null)
        {
            return NotFound("Produtos não encontrados...");
        }
        else
        {
            return produtos;
        }       
    }

    [HttpGet("{id:int}", Name = "ObterProduto")] //Name="ObterProduto"
    public ActionResult<Produto> Get(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
        if(produto is null)
        {
            return NotFound("Produto não encontrado..."); // Notfound retorna 404
        }
        else
        {
            return produto;
        }
    }

    [HttpPost]// reposta padrão do POST é o cod 201
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest(); // Erro 404

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return Ok();
            //new CreatedAtRouteResult("ObterProduto",
            //new { id = produto.ProdutoId }, produto);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
       
        if (id != produto.ProdutoId)
        {
            return BadRequest(); // Erro 404
        }

        _context.Entry(produto).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(produto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
       var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

        if (produto is null)
        {
            return BadRequest("Produto não localizado..."); // Erro 404
        }

        _context.Produtos.Remove(produto);
        _context.SaveChanges();

        return Ok(produto);
    }
   
}
