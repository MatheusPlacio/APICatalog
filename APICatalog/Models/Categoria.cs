using System.ComponentModel.DataAnnotations;

namespace APICatalog.Models;

public class Categoria
{
    #region Propriedades
    public int CategoriaId  { get; set; } // Chave primária

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    public ICollection<Produto>? Produtos { get; set; } // Cada categoria pode ter uma coleção de produtos
    #endregion

    #region Construtor
    public Categoria()
    {
        Produtos = new List<Produto>();
    }
    #endregion
}

