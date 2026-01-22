using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public record tipos_siniestro
    {
        [Key]
        public int tipos_siniestro_id { get; set; }
        public string nombre { get; set; }
    }
}
