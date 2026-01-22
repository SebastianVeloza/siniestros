using Domain.Interfaces.Repositories;
using Domain.Interfaces;
using Moq;
using Application.Handlers.SiniestrosH;
using Application.Commands.siniestrosC;
using Application.DTO.Request;
using Domain.Entities;

namespace Test.Commands
{
    public class CreateSiniestroCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Create_Siniestro_And_Commit_Transaction()
        {
            // Arrange
            var uowMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<ISiniestrosRepository>();

            uowMock.SetupGet(x => x.siniestrosRepository)
                   .Returns(repoMock.Object);

            uowMock.Setup(x => x.BeginTransactionAsync())
                   .Returns(Task.CompletedTask);

            uowMock.Setup(x => x.CommitTransactionAsync())
                   .Returns(Task.CompletedTask);

            uowMock.Setup(x => x.SaveChangesAsync())
                   .ReturnsAsync(1);

            var handler = new CreateSiniestrosCommandHandler(uowMock.Object);

            var request = new SiniestrosRequest
            {
                fechahora = DateTime.Now,
                departamentos_id = 1,
                ciudades_id = 1,
                tipos_siniestro_id = 1,
                vehiculos_involucrados = 2,
                numero_victimas = 1,
                descripcion = "Test siniestro"
            };

            var command = new CreateSiniestrosCommand(request);


            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result);

            uowMock.Verify(x => x.BeginTransactionAsync(), Times.Once);
            repoMock.Verify(x => x.Add(It.IsAny<Siniestros>()), Times.Once);
            uowMock.Verify(x => x.SaveChangesAsync(), Times.Once);
            uowMock.Verify(x => x.CommitTransactionAsync(), Times.Once);
            uowMock.Verify(x => x.RollbackTransactionAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Rollback_When_Exception_Occurs()
        {
            // Arrange
            var uowMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<ISiniestrosRepository>();

            uowMock.SetupGet(x => x.siniestrosRepository)
                   .Returns(repoMock.Object);

            uowMock.Setup(x => x.BeginTransactionAsync())
                   .Returns(Task.CompletedTask);

            uowMock.Setup(x => x.SaveChangesAsync())
                   .ThrowsAsync(new Exception("DB error"));

            uowMock.Setup(x => x.RollbackTransactionAsync())
                   .Returns(Task.CompletedTask);

            var handler = new CreateSiniestrosCommandHandler(uowMock.Object);

            var request = new SiniestrosRequest
            {
                fechahora = DateTime.Now,
                departamentos_id = 1,
                ciudades_id = 1,
                tipos_siniestro_id = 1,
                vehiculos_involucrados = 1,
                numero_victimas = 0
            };

            var command = new CreateSiniestrosCommand(request);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                handler.Handle(command, CancellationToken.None));

            uowMock.Verify(x => x.RollbackTransactionAsync(), Times.Once);
            uowMock.Verify(x => x.CommitTransactionAsync(), Times.Never);
        }
    }
}
