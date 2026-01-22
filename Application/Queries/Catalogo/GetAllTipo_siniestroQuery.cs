using Application.DTO.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Catalogo
{
    public record GetAllTipo_siniestroQuery(int Page, int PageSize) : IRequest<PagedResult<tipos_siniestro>>;
}
