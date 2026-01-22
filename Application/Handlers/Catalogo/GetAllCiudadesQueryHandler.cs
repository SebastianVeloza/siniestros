using Application.DTO.Pagination;
using Application.Queries.CiudadQ;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers.Departamento_ciudades
{
    internal class GetAllCiudadesQueryHandler : IRequestHandler<GetAllCiudadesQuery, PagedResult<ciudades>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCiudadesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<PagedResult<ciudades>> Handle(GetAllCiudadesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
