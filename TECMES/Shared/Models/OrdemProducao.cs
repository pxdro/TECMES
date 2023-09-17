using System.ComponentModel.DataAnnotations;
using TECMES.Shared.Enums;

namespace TECMES.Shared.Models
{
    public class OrdemProducao : Entity
    {
        [Required(ErrorMessage = "Quantidade da ordem de produção é obrigatória.")]
        public int Quantidade { get; set; } = 0;

        [Required(ErrorMessage = "Liberado da ordem de produção é obrigatório.")]
        public EnumStatus Liberado { get; set; }

        public Guid? ProdutoId { get; set; }

        public Produto? Produto { get; set; }
    }
}
