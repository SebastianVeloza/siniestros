using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public record ciudades
    {
        [Key]
        public int ciudades_id { get; set; }
        public string nombre { get; set; }
        public int departamentos_id { get; set; }
        public virtual departamentos Departamentos { get; set; }
    }
}
