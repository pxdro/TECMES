using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TECMES.Shared.Enums;

namespace TECMES.Shared.Models
{
    public class Usuario : Entity
    {
        [Required(ErrorMessage = "Nome do usuário é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome do usuário deve conter até 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email do usuário é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O email do usuário deve conter até 100 caracteres.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Senha do usuário é obrigatória.")]
        [MaxLength(255, ErrorMessage = "O email do usuário deve conter até 255 caracteres.")]
        public string Senha { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status do usuário é obrigatório.")]
        public EnumStatus Status { get; set; }

        [Required(ErrorMessage = "Acesso a ordem de produção do usuário é obrigatório.")]
        public EnumStatus OrdemProducao { get; set; }

        [NotMapped]
        public string Jwt { get; set; } = string.Empty;

        [NotMapped]
        public string Error { get; set; } = string.Empty;
    }
}
