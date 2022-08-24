using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalog.Models;

public class Produto
{

    #region Propriedades
    public int ProdutoId { get; set; } // Chave primária


    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }



    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }

    //
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }



    [Required]
    [StringLength(80)]
    public string? ImagemUrl { get; set; }



    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; } // Produto está mapeado para uma categoria
    #endregion
}
