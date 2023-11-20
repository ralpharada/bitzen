using Bitzen.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Bitzen.Domain.Models
{
    public class RefreshToken : IRefreshToken
    {
        [Key]
        public string Token { get; set; } = null!;
        public int UsuarioId { get; set; }
        public DateTime DataExpiracao { get; set; }
    }
}
