using SistemasConsultas.Models;
using System.ComponentModel.DataAnnotations;

namespace SistemasConsultas.Models
{
    public class Consulta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        [StringLength(40, ErrorMessage = "A especialidade deve ter no máximo 40 caracteres.")]
        public string Especialidade { get; set; }

        [Required(ErrorMessage = "A data e hora são obrigatórias.")]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; }

        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }
    }
}