using Application.Handlers.SiniestrosH;
using Application.Queries.SiniestrosQ;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces;
using Moq;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Test.Queries
{
    public class GetSiniestrosQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Paged_Siniestros()
        {
            // Arrange
            var data = new List<Siniestros>
            {
                new()
                {
                Siniestros_id = Guid.NewGuid(),
                    fechahora = DateTime.Now,
                departamentos_id = 1,
                Departamentos = new departamentos { nombre = "Antioquia" },
                ciudades_id = 1,
                Ciudades = new ciudades { nombre = "Medellín" },
                tipos_siniestro_id = 1,
                Tipos_Siniestro = new tipos_siniestro { nombre = "Choque" }
                },
                new()
                {
                Siniestros_id = Guid.NewGuid(),
                    fechahora = DateTime.Now.AddDays(-1),
                departamentos_id = 1,
                    Departamentos = new departamentos { nombre = "Antioquia" },
                ciudades_id = 1,
                Ciudades = new ciudades { nombre = "Medellín" },
                tipos_siniestro_id = 1,
                Tipos_Siniestro = new tipos_siniestro { nombre = "Choque" }
            }
        }.AsQueryable();

            var repoMock = new Mock<ISiniestrosRepository>();
            var uowMock = new Mock<IUnitOfWork>();

            repoMock.Setup(x => x.Query()).Returns(data);
            repoMock.Setup(x => x.CountAsync(It.IsAny<IQueryable<Siniestros>>()))
                    .ReturnsAsync(data.Count());

            uowMock.SetupGet(x => x.siniestrosRepository)
                   .Returns(repoMock.Object);

            var handler = new GetSiniestrosQueryHandler(uowMock.Object);

            var query = new GetSiniestrosQuery(
                        departamentos_id: 1,
                        FechaInicio: null,
                        FechaFin: null,
                        Page: 1,
                        PageSize: 10
                    );

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalItems);
            Assert.Equal(2, result.Items.Count);

            repoMock.Verify(x => x.Query(), Times.Once);
            repoMock.Verify(x => x.CountAsync(It.IsAny<IQueryable<Siniestros>>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Filter_By_Date_Range()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new Context(options);

            var now = new DateTime(2024, 1, 10, 10, 0, 0);

            context.Siniestros.AddRange(
                new Siniestros { Siniestros_id = Guid.NewGuid(), fechahora = now },
                new Siniestros { Siniestros_id = Guid.NewGuid(), fechahora = now.AddDays(-10) }
            );

            await context.SaveChangesAsync();

            var uow = new UnitOfWork(context);
            var handler = new GetSiniestrosQueryHandler(uow);

            var query = new GetSiniestrosQuery(
                departamentos_id: null,
                FechaInicio: DateTime.Today.AddDays(-1),
                FechaFin: DateTime.Today.AddDays(1),
                Page: 1,
                PageSize: 10
            );

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Empty(result.Items);
        }

        [Fact]
        public async Task Handle_Should_Filter_By_Departamento()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new Context(options);

            var depto = new departamentos { departamentos_id = 1, nombre = "Antioquia" };
            context.departamentos.Add(depto);

            context.Siniestros.AddRange(
                new Siniestros
                {
                    Siniestros_id = Guid.NewGuid(),
                    departamentos_id = 1,
                    Departamentos = depto
                },
                new Siniestros
                {
                    Siniestros_id = Guid.NewGuid(),
                    departamentos_id = 2
                }
            );

            await context.SaveChangesAsync();

            var uow = new UnitOfWork(context);
            var handler = new GetSiniestrosQueryHandler(uow);

            var query = new GetSiniestrosQuery(
                departamentos_id: 1,
                FechaInicio: null,
                FechaFin: null,
                Page: 1,
                PageSize: 10
            );

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Empty(result.Items);
        }

        [Fact]
        public async Task Handle_Should_Return_Correct_Page()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new Context(options);

            var baseDate = new DateTime(2024, 1, 10, 12, 0, 0);

            for (int i = 0; i < 20; i++)
            {
                context.Siniestros.Add(new Siniestros
                {
                    Siniestros_id = Guid.NewGuid(),
                    fechahora = baseDate.AddMinutes(-i)
                });
            }

            await context.SaveChangesAsync();

            var uow = new UnitOfWork(context);
            var handler = new GetSiniestrosQueryHandler(uow);

            var query = new GetSiniestrosQuery(
                departamentos_id: null,
                FechaInicio: null,
                FechaFin: null,
                Page: 2,
                PageSize: 5
            );

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(0, result.Items.Count);
            Assert.Equal(20, result.TotalItems);

        }
    }
}
