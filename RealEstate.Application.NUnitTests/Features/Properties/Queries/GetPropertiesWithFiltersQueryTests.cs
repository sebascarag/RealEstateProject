using AutoFixture;
using AutoMapper;
using NUnit.Framework;
using RealEstate.Application.Contracts;
using RealEstate.Application.Features.Properties.Queries;
using RealEstate.Application.Mapping;
using RealEstate.Application.NUnitTests.Mocks;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.NUnitTests.Features.Properties.Queries
{
    [TestFixture]
    public class GetPropertiesWithFiltersQueryTests
    {
        private IRepository<Property> _propertyRepoMock;
        private readonly IMapper _mapper;
        private readonly Fixture _fixture;

        public GetPropertiesWithFiltersQueryTests()
        {
            var mapConfig = new MapperConfiguration(c => c.AddProfile(new MappingProfile()));
            _mapper = mapConfig.CreateMapper();

            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [SetUp]
        public async Task SetUp()
        {
            _propertyRepoMock = MockRepository<Property>.GetMockIRepository();

            // add test base data
            var testData = _fixture.CreateMany<Property>().ToList();
            _propertyRepoMock.AddRange(testData);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);
        }

        [Test]
        public async Task GetPropertiesWithFilters_InputNone_ReturnsPropetyList()
        {
            // Arrange
            GetPropertiesWithFiltersQueryRequest request = new();
            GetPropertiesWithFiltersQueryHandler handler = new(_propertyRepoMock, _mapper);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.AtLeast(1));
                Assert.That(result, Is.TypeOf<List<PropertyFilteredDto>>());
            });
        }

        [Test]
        [TestCase("Corsair Real Estate", "Corsair")]
        [TestCase("Fortune Team", "Fortune Team")]
        [TestCase("Pilot Property Group", "Pil")]
        public async Task GetPropertiesWithFiltersQuery_InputExistingPropertyName_ReturnsPropetyList(string propertyName, string seachValue)
        {
            // Arrange
            var property = _fixture.Build<Property>()
                                .With(p => p.Name, propertyName)
                                .With(p => p.Active, true).Create();
            _propertyRepoMock.Add(property);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);

            GetPropertiesWithFiltersQueryRequest request = new() { Name = seachValue };
            GetPropertiesWithFiltersQueryHandler handler = new(_propertyRepoMock, _mapper);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.AtLeast(1));
                Assert.That(result, Is.TypeOf<List<PropertyFilteredDto>>());
                Assert.Contains(property.Name, result.Select(x => x.Name).ToList());
            });
        }

        [Test]
        [TestCase("Corsair Real Estate", "xxx")]
        [TestCase("Fortune Team", "yyy")]
        [TestCase("Pilot Property Group", "zzz")]
        public async Task GetPropertiesWithFiltersQuery_InputNotExistingPropertyName_ReturnsEmptyPropetyList(string propertyName, string seachValue)
        {
            // Arrange
            var property = _fixture.Build<Property>()
                                .With(p => p.Name, propertyName)
                                .With(p => p.Active, true).Create();
            _propertyRepoMock.Add(property);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);

            GetPropertiesWithFiltersQueryRequest request = new() { Name = seachValue };
            GetPropertiesWithFiltersQueryHandler handler = new(_propertyRepoMock, _mapper);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.EqualTo(0));
                Assert.That(result, Is.TypeOf<List<PropertyFilteredDto>>());
            });
        }

        [Test]
        [TestCase("2607 Trails End Road Fort Lauderdale, FL 33383", "2607 Trails End Road Fort Lauderdale, FL 33383")]
        [TestCase("4611 Frosty Lane Sidney, NY 13838", "NY 13838")]
        [TestCase("1646 Cherry Tree Drive Hastings, FL 32145", "Cherry")]
        public async Task GetPropertiesWithFiltersQuery_InputExistingPropertyAddress_ReturnsPropetyList(string propertyAddress, string seachValue)
        {
            // Arrange
            var property = _fixture.Build<Property>()
                                .With(p => p.Address, propertyAddress)
                                .With(p => p.Active, true).Create();
            _propertyRepoMock.Add(property);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);

            GetPropertiesWithFiltersQueryRequest request = new() { Address = seachValue };
            GetPropertiesWithFiltersQueryHandler handler = new(_propertyRepoMock, _mapper);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.AtLeast(1));
                Assert.That(result, Is.TypeOf<List<PropertyFilteredDto>>());
                Assert.Contains(property.Address, result.Select(x => x.Address).ToList());
            });
        }

        [Test]
        [TestCase("2607 Trails End Road Fort Lauderdale, FL 33383", "123 xxxx yyyyyyy")]
        [TestCase("4611 Frosty Lane Sidney, NY 13838", "xxx 13838")]
        [TestCase("1646 Cherry Tree Drive Hastings, FL 32145", "Abc")]
        public async Task GetPropertiesWithFiltersQuery_InputNotExistingPropertyAddress_ReturnsPropetyList(string propertyAddress, string seachValue)
        {
            // Arrange
            var property = _fixture.Build<Property>()
                                .With(p => p.Address, propertyAddress)
                                .With(p => p.Active, true).Create();
            _propertyRepoMock.Add(property);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);

            GetPropertiesWithFiltersQueryRequest request = new() { Address = seachValue };
            GetPropertiesWithFiltersQueryHandler handler = new(_propertyRepoMock, _mapper);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.EqualTo(0));
                Assert.That(result, Is.TypeOf<List<PropertyFilteredDto>>());
            });
        }

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        [TestCase(13246)]
        public async Task GetPropertiesWithFiltersQuery_InputMinPrice_ReturnsPropetyList(int minPrice)
        {
            // Arrange
            var property = _fixture.Build<Property>()
                                .With(p => p.Price, minPrice)
                                .With(p => p.Active, true).Create();
            _propertyRepoMock.Add(property);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);

            GetPropertiesWithFiltersQueryRequest request = new() { MinPrice = minPrice};
            GetPropertiesWithFiltersQueryHandler handler = new(_propertyRepoMock, _mapper);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.AtLeast(1));
                Assert.That(result, Is.TypeOf<List<PropertyFilteredDto>>());
                Assert.True(result.Select(x => x.Price).All(p => p >= minPrice));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(13246)]
        public async Task GetPropertiesWithFiltersQuery_InputMaxPrice_ReturnsPropetyList(int maxPrice)
        {
            // Arrange
            var property = _fixture.Build<Property>()
                                .With(p => p.Price, maxPrice)
                                .With(p => p.Active, true).Create();
            _propertyRepoMock.Add(property);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);

            GetPropertiesWithFiltersQueryRequest request = new() { MaxPrice = maxPrice };
            GetPropertiesWithFiltersQueryHandler handler = new(_propertyRepoMock, _mapper);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.AtLeast(1));
                Assert.That(result, Is.TypeOf<List<PropertyFilteredDto>>());
                Assert.True(result.Select(x => x.Price).All(p => p <= maxPrice));
            });
        }


        [Test]
        [TestCase(0, 1)]
        [TestCase(10, 200)]
        [TestCase(1234, 5678)]
        public async Task GetPropertiesWithFiltersQuery_InputMinMaxPrice_ReturnsPropetyList(int minPrice, int maxPrice)
        {
            // Arrange
            var propertyTestList = new List<Property>
            {
                _fixture.Build<Property>()
                                .With(p => p.Price, minPrice)
                                .With(p => p.Active, true).Create(),
                _fixture.Build<Property>()
                                .With(p => p.Price, maxPrice)
                                .With(p => p.Active, true).Create()
            };

            _propertyRepoMock.AddRange(propertyTestList);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);

            GetPropertiesWithFiltersQueryRequest request = new() { MinPrice = minPrice, MaxPrice = maxPrice };
            GetPropertiesWithFiltersQueryHandler handler = new(_propertyRepoMock, _mapper);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.AtLeast(1));
                Assert.That(result, Is.TypeOf<List<PropertyFilteredDto>>());
                Assert.True(result.Select(x => x.Price).All(p => p >= minPrice && p <= maxPrice));
            });
        }

        [Test]
        [TestCase(0)]
        [TestCase(1990)]
        [TestCase(2023)]
        public async Task GetPropertiesWithFiltersQuery_InputMinYear_ReturnsPropetyList(int minYear)
        {
            // Arrange
            var property = _fixture.Build<Property>()
                                .With(p => p.Year, minYear)
                                .With(p => p.Active, true).Create();
            _propertyRepoMock.Add(property);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);

            GetPropertiesWithFiltersQueryRequest request = new() { MinYear = minYear };
            GetPropertiesWithFiltersQueryHandler handler = new(_propertyRepoMock, _mapper);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.AtLeast(1));
                Assert.That(result, Is.TypeOf<List<PropertyFilteredDto>>());
                Assert.True(result.Select(x => x.Year).All(p => p >= minYear));
            });
        }

        [Test]
        [TestCase(1990)]
        [TestCase(2000)]
        [TestCase(2023)]
        public async Task GetPropertiesWithFiltersQuery_InputMaxYear_ReturnsPropetyList(int maxYear)
        {
            // Arrange
            var property = _fixture.Build<Property>()
                                .With(p => p.Year, maxYear)
                                .With(p => p.Active, true).Create();
            _propertyRepoMock.Add(property);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);

            GetPropertiesWithFiltersQueryRequest request = new() { MaxYear = maxYear };
            GetPropertiesWithFiltersQueryHandler handler = new(_propertyRepoMock, _mapper);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.AtLeast(1));
                Assert.That(result, Is.TypeOf<List<PropertyFilteredDto>>());
                Assert.True(result.Select(x => x.Year).All(p => p <= maxYear));
            });
        }

        [Test]
        [TestCase(0, 2000)]
        [TestCase(1990, 2015)]
        [TestCase(2023, 2050)]
        public async Task GetPropertiesWithFiltersQuery_InputMinMaxYear_ReturnsPropetyList(int minYear, int maxYear)
        {
            // Arrange
            var propertyTestList = new List<Property>
            {
                _fixture.Build<Property>()
                                .With(p => p.Year, minYear)
                                .With(p => p.Active, true).Create(),
                _fixture.Build<Property>()
                                .With(p => p.Year, maxYear)
                                .With(p => p.Active, true).Create()
            };

            _propertyRepoMock.AddRange(propertyTestList);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);

            GetPropertiesWithFiltersQueryRequest request = new() { MinYear = minYear, MaxYear = maxYear };
            GetPropertiesWithFiltersQueryHandler handler = new(_propertyRepoMock, _mapper);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.AtLeast(1));
                Assert.That(result, Is.TypeOf<List<PropertyFilteredDto>>());
                Assert.True(result.Select(x => x.Year).All(p => p >= minYear && p <= maxYear));
            });
        }

        [Test]
        [TestCase("Elinor J. Malone", "Elinor J. Malone")]
        [TestCase("Rose Barela", "Rose")]
        [TestCase("Michael M. Cunningham", "Cunningham")]
        public async Task GetPropertiesWithFiltersQuery_InputPropertyOwnerName_ReturnsPropetyList(string propertyOwnerName, string seachValue)
        {
            // Arrange
            var property = _fixture.Build<Property>()
                                .With(p => p.Owner, _fixture.Build<Owner>().With(o => o.Name, propertyOwnerName).Create())
                                .With(p => p.Active, true).Create();
            _propertyRepoMock.Add(property);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);

            GetPropertiesWithFiltersQueryRequest request = new() { OwnerName = seachValue };
            GetPropertiesWithFiltersQueryHandler handler = new(_propertyRepoMock, _mapper);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Count, Is.AtLeast(1));
                Assert.That(result, Is.TypeOf<List<PropertyFilteredDto>>());
                Assert.Contains(propertyOwnerName, result.Select(p => p.OwnerName).ToList());
            });
        }
    }
}
