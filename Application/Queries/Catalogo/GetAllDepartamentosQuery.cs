using Application.DTO.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Catalogo
{
    public record GetAllDepartamentosQuery(string? nombre, int PageNumber = 1, int PageSize = 10) : IRequest<CountPage<departamentos>>;
}
