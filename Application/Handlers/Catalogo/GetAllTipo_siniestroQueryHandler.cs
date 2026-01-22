using Application.DTO.Pagination;
using Application.Queries.Catalogo;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers.Catalogo
{
    internal class GetAllTipo_siniestroQueryHandler : IRequestHandler<GetAllTipo_siniestroQuery, CountPage<tipos_siniestro>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTipo_siniestroQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CountPage<tipos_siniestro>> Handle(GetAllTipo_siniestroQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.tipos_SiniestroRepository.GetAllAsync();
                var filtro = result.Where(x => string.IsNullOrWhiteSpace(request.nombre) || x.nombre.Contains(request.nombre)).ToList();

                return new CountPage<tipos_siniestro>
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
