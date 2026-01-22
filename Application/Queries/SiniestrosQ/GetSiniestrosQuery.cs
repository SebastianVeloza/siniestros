using Application.DTO.Pagination;
using Application.DTO.Response;
using MediatR;

namespace Application.Queries.SiniestrosQ
{
    public record GetSiniestrosQuery(int? departamentos_id, DateTime? FechaInicio, DateTime? FechaFin, int Page, int PageSize) : IRequest<PagedResult<SiniestroResponse>>;
}
