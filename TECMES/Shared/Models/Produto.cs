using System.ComponentModel.DataAnnotations;

namespace TECMES.Shared.Models
{
    public class Produto : Entity
    {
        [Required(ErrorMessage = "Nome do produto é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome do produto deve conter até 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
    }
}
