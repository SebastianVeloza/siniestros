namespace Application.DTO.Request
{
    public class SiniestrosRequest
    {
        public DateTime fechahora { get; set; }
        public int departamentos_id { get; set; }
        public int ciudades_id { get; set; }
        public int tipos_siniestro_id { get; set; }
        public int vehiculos_involucrados { get; set; }
        public int numero_victimas { get; set; }
        public string? descripcion { get; set; }
    }
}
