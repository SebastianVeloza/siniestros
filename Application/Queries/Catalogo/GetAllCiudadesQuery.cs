using Application.DTO.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries.CiudadQ
{
    public record GetAllCiudadesQuery(int Page, int PageSize) : IRequest<PagedResult<ciudades>>;
}
