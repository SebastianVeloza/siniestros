using Application.DTO.Pagination;
using Application.Queries.Catalogo;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Departamento_ciudades
{
    internal class GetAllDepartamentosQueryHandler: IRequest<GetAllDepartamentosQuery, PagedResult<departamentos>>
    {
    }
}
