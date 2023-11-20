using System.ComponentModel.DataAnnotations;

namespace Bitzen.Domain.Interfaces
{
    public interface IRefreshToken
    {
        [Key]
        string Token { get; set; }
        int UsuarioId { get; set; }
        DateTime DataExpiracao { get; set; }
    }

}
