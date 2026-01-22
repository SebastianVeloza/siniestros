using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public record Logs_Siniestros
    {
        [Key]
        public int Logs_Siniestros_id { get; set; }
        public DateTime fechahora { get; set; }
        public string accion { get; set; }
        public string envio { get; set; }
        public string tabla { get; set; }
    }
}
