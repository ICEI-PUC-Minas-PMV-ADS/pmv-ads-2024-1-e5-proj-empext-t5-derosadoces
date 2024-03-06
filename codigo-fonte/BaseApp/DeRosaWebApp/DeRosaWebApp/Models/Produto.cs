using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DeRosaWebApp.Models
{
    public class Produto
    {
        [Key]
        [DisplayName("Código produto")]
        public int Cod_Produto { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório!")]
        [StringLength(100, ErrorMessage = "O Tamanho máximo do nome é de 100 caractéres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O preço é requerido!")]
        [DisplayName("Preço")]
        public double Preco { get; set; }
        [Required(ErrorMessage = " A quantidade de produtos não pode ser 0")]
        public int Quantidade { get; set; }
        [DisplayName("Imagem")]
        public string ImagemUrl { get; set; }
        [Required(ErrorMessage = " A descrição é obrigatória!")]
        [MaxLength(208)]
        [DisplayName("Descrição curta")]
        public string DescricaoCurta { get; set; }
        [Required(ErrorMessage = "Preço secundário é obrigatório!")]
        [Column(TypeName = "decimal(10,2)")]
        [DisplayName("Preço secundario")]
        public decimal PrecoSecundario { get; set; }
        [Required]
        [DisplayName("Categoria")]
        public int IdCategoria { get; set; }


    }
}
