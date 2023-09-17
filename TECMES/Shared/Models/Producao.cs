using System.ComponentModel.DataAnnotations;

namespace TECMES.Shared.Models
{
    public class Producao : Entity
    {
        [Required(ErrorMessage = "Produzidos de produção é obrigatória.")]
        public int Produzido { get; set; } = 0;

        public Guid? OrdemProducaoId { get; set; }

        public OrdemProducao? OrdemProducao { get; set; }
    }
}
