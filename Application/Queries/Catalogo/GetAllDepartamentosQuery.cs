using Application.DTO.Pagination;
using Domain.Entities;
using MediatR;

namespace Application.Queries.Catalogo
{
    public record GetAllDepartamentosQuery(int Page, int PageSize) : IRequest<PagedResult<departamentos>>;
}
