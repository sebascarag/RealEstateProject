using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using RealEstate.Application.Contracts;
using RealEstate.DataAccess;
using RealEstate.DataAccess.Interceptors;
using RealEstate.DataAccess.Repository;

namespace RealEstate.Application.NUnitTests.Mocks
{
    public static class MockRepository<T> where T : class
    {
        public static IRepository<T> GetMockIRepository()
        {
            var repositoryMock = GetMockRepository();
            return repositoryMock.Object;
        }

        public static Mock<Repository<T>> GetMockRepository()
        {
            // create db context in memory
            var options = new DbContextOptionsBuilder<RealEstateDbContext>()
                                .UseInMemoryDatabase(databaseName: $"StreamerDbContext-{Guid.NewGuid}").Options;

            var currentUserServiceMock = new Mock<ICurrentUserService>();
            currentUserServiceMock.Setup(x => x.UserName).Returns("unit-test");

            var auditInterceptor = new AuditableEntitySaveChangesInterceptor(currentUserServiceMock.Object);

            var dbContextFake = new RealEstateDbContext(options, auditInterceptor);
            dbContextFake.Database.EnsureDeleted();

            // create repository
            var repositoryMock = new Mock<Repository<T>>(dbContextFake);
            return repositoryMock;
        }
    }
}
