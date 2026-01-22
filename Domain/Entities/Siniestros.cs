using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public record Siniestros
    {
        [Key]
        public int Siniestros_id { get; set; }
        public DateTime fechahora { get; set; }
        public int departamentos_id { get; set; }
        public int ciudades_id { get; set; }
        public int tipos_siniestro_id { get; set; }
        public int vehiculos_involucrados { get; set; }
        public int numero_victimas { get; set; }
        public string? descripcion { get; set; }
        public virtual departamentos Departamentos { get; set; }
        public virtual ciudades Ciudades { get; set; }
        public virtual tipos_siniestro Tipos_Siniestro { get; set; }
    }
}
