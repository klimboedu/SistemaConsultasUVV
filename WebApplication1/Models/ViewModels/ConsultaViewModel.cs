using System.ComponentModel.DataAnnotations;

namespace SistemasConsultas.Models.ViewModels
{
    public class ConsultaViewModel
    {
        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        [StringLength(100, ErrorMessage = "A especialidade deve ter no máximo 100 caracteres.")]
        [Display(Name = "Especialidade")]
        public string Especialidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data e hora são obrigatórias.")]
        [Display(Name = "Data e Hora")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;
    }
}