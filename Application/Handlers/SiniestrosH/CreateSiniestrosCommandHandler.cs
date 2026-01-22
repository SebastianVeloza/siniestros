using Application.Commands.siniestrosC;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers.SiniestrosH
{
    public class CreateSiniestrosCommandHandler : IRequestHandler<CreateSiniestrosCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSiniestrosCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateSiniestrosCommand siniestrorequest, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var request = siniestrorequest.Siniestro;
                var siniestro = new Siniestros
                {
                    Siniestros_id = Guid.NewGuid(),
                    fechahora = request.fechahora,
                    departamentos_id = request.departamentos_id,
                    ciudades_id = request.ciudades_id,
                    tipos_siniestro_id = request.tipos_siniestro_id,
                    vehiculos_involucrados = request.vehiculos_involucrados,
                    numero_victimas = request.numero_victimas,
                    descripcion = request.descripcion
                };

                 _unitOfWork.siniestrosRepository.Add(siniestro);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return siniestro.Siniestros_id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
