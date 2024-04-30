using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace DeRosaWebApp.Models
{
    public class Produto
    {
        [Key]
        [DisplayName("Código do Produto")]
        public int Cod_Produto { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório!")]
        [StringLength(100, ErrorMessage = "O tamanho máximo do nome é de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        [DisplayName("Preço")]
        public double Preco { get; set; }

        [Range(0, 999,ErrorMessage ="O máximo de estoque é 999")]
        [DisplayName("Quantidade no Estoque")]
        public int EmEstoque { get; set; }

        [DisplayName("Imagem do Produto")]
        [Required(ErrorMessage = "A imagem do produto é obrigatória!")]
        public string ImagemUrl { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória!")]
        [MaxLength(208, ErrorMessage = "A descrição deve ter no máximo 208 caracteres.")]
        [MinLength(20,ErrorMessage ="A descrição deve conter no mínimo 20 caracteres")]
        [DisplayName("Descrição Curta")]
        public string DescricaoCurta { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço secundário deve ser no máximo 999")]
        [DisplayName("Preço Secundário")]
        public decimal PrecoSecundario { get; set; }

        [Required(ErrorMessage = "A categoria do produto é obrigatória!")]
        [DisplayName("Categoria")]
        public int IdCategoria { get; set; }

        [DisplayName("Produto da Semana")]
        public bool ProdutoDaSemana { get; set; }
        [DisplayName("Bloquear pedidos deste produto")]
        public bool Indisponivel { get; set; }

    }
}
