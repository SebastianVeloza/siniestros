using Application.DTO.Pagination;
using Application.DTO.Response;
using Application.Queries.SiniestrosQ;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers.SiniestrosH
{
    public class GetSiniestrosQueryHandler : IRequestHandler<GetSiniestrosQuery, PagedResult<SiniestroResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSiniestrosQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<SiniestroResponse>> Handle(GetSiniestrosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _unitOfWork.siniestrosRepository.Query();

                if (request.departamentos_id.HasValue)
                    query = query.Where(x => x.departamentos_id == request.departamentos_id);

                if (request.FechaInicio.HasValue)
                    query = query.Where(x => x.fechahora >= request.FechaInicio);

                if (request.FechaFin.HasValue)
                    query = query.Where(x => x.fechahora <= request.FechaFin);

                var total = await _unitOfWork.siniestrosRepository.CountAsync(query);

                var items =  query
                    .OrderByDescending(x => x.fechahora)
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new SiniestroResponse
                    {
                        Siniestros_id = x.Siniestros_id,
                        fechahora = x.fechahora,
                        departamentos_id = x.departamentos_id,
                        nombre_departamento = x.Departamentos.nombre,
                        ciudades_id = x.ciudades_id,
                        nombre_ciudad = x.Ciudades.nombre,
                        tipos_siniestro_id = x.tipos_siniestro_id,
                        nombre_tipos_siniestro = x.Tipos_Siniestro.nombre,
                        vehiculos_involucrados = x.vehiculos_involucrados,
                        numero_victimas = x.numero_victimas,
                        descripcion = x.descripcion
                    })
                    .ToList();

                return new PagedResult<SiniestroResponse>(
                    items, total, request.Page, request.PageSize);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
