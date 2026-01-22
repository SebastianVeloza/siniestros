using Application.DTO.Request;
using MediatR;

namespace Application.Commands.siniestrosC
{
    public record CreateSiniestrosCommand(SiniestrosRequest Siniestro): IRequest<Guid>;
}
