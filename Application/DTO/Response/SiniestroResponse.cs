namespace Application.DTO.Response
{
    public class SiniestroResponse
    {
        public Guid Siniestros_id { get; set; }
        public DateTime fechahora { get; set; }
        public int departamentos_id { get; set; }
        public string nombre_departamento { get; set; }
        public int ciudades_id { get; set; }
        public string nombre_ciudad { get; set; }
        public int tipos_siniestro_id { get; set; }
        public string nombre_tipos_siniestro { get; set; }
        public int vehiculos_involucrados { get; set; }
        public int numero_victimas { get; set; }
        public string? descripcion { get; set; }
    }
}
