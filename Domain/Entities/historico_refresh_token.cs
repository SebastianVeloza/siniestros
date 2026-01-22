using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public record historico_refresh_token
    {
        [Key]
        public int historico_refresh_token_id { get; set; }
        public string token { get; set; }
        public string refresh_token { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_expiracion { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool activo { get; set; }
    }
}
