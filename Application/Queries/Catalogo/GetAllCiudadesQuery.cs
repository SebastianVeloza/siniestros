using Application.DTO.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries.CiudadQ
{
    public record GetAllCiudadesQuery(string? nombre,int PageNumber = 1, int PageSize = 10) : IRequest<CountPage<ciudades>>;
}
