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
                {
                    if (request.FechaInicio > request.FechaFin)
                    {
                        throw new Exception("|La fecha incial no puede ser mayor a la fecha final.");
                    }
                    if (!request.FechaFin.HasValue)
                    {
                        throw new Exception("|Debe llenar la fecha final tambien.");
                    }
                    query = query.Where(x => x.fechahora.Date >= request.FechaInicio.Value.Date);
                }

                if (request.FechaFin.HasValue)
                {
                    if (!request.FechaInicio.HasValue)
                    {
                        throw new Exception("|Debe llenar la fecha de inicio tambien.");
                    }
                    var fechaFin = request.FechaFin.Value.Date.AddDays(1).AddTicks(-1);
                    query = query.Where(x => x.fechahora <= fechaFin);
                }

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
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("|") == false)
                {
                    throw;
                }
                throw new Exception(ex.Message);

            }
        }
    }
}
