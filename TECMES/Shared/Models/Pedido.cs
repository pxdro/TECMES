using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TECMES.Shared.Models
{
    public class Pedido : Entity
    {
        [Required(ErrorMessage = "Cliente do pedido é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O Cliente do pedido deve conter até 100 caracteres.")]
        public string Cliente { get; set; } = string.Empty;

        [Required(ErrorMessage = "Produto do pedido é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O Cliente do pedido deve conter até 100 caracteres.")]
        public string Produto { get; set; } = "Temp";

        [Required(ErrorMessage = "Quantidade do pedido é obrigatório.")]
        public int Quantidade { get; set; } = 0;

        [NotMapped]
        public Guid? ProducaoId { get; set; } = null;
    }
}
