using APICatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalog.Context;

public class APICatalogContext : DbContext
{
    //Construtor \/
    public APICatalogContext(DbContextOptions<APICatalogContext> options) : base(options)
    { }

    // Mapeamento das minhas entidades \/
    public DbSet<Categoria>? Categorias { get; set; }
    public DbSet <Produto>? Produtos { get; set; }

}
