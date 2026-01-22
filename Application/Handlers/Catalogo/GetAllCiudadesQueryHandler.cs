using Application.DTO.Pagination;
using Application.Queries.CiudadQ;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers.Departamento_ciudades
{
    internal class GetAllCiudadesQueryHandler : IRequestHandler<GetAllCiudadesQuery, CountPage<ciudades>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCiudadesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CountPage<ciudades>> Handle(GetAllCiudadesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.ciudadesRepository.GetallCiudades();
                var filtro = result.Where(x => string.IsNullOrWhiteSpace(request.nombre) || x.nombre.Contains(request.nombre)).ToList();

                return new CountPage<ciudades>
                {
                    TotalCount = filtro.Count(),
                    Items = filtro.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList()
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
