using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioViceri.Models
{
    [Table("Usuarios")]
    public class UsuarioModel
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "CPF Obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Nome Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email Obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha Obrigatória")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Data de Nascimento Obrigatória")]
        public DateTime? DataNascimento { get; set; }

    }
}
