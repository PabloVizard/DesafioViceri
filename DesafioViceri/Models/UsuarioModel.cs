using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioViceri.Models
{
    [Table("Usuarios")]
    public class UsuarioModel
    {
        [Key]
        public int UsuarioId { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

    }
}
