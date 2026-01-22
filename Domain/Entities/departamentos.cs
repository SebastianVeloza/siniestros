using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public record departamentos
    {
        [Key]
        public int departamentos_id { get; set; }
        public string nombre { get; set; }
    }
}
