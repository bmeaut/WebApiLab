using System.Net;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Text.Json;
using WebApiLab.Bll.Dtos;
using System.Transactions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace WebApiLab.Tests
{
    public class ProductControllerTests : IClassFixture
        <CustomWebApplicationFactory>
    {
        private readonly Faker<Product> _dtoFaker;
        private readonly WebApplicationFactory<Program> _appFactory;
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly ITestOutputHelper _testOutput;

        public ProductControllerTests(CustomWebApplicationFactory appFactory
            , ITestOutputHelper output)
        {
            _appFactory = appFactory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddXUnit(output);
                    });
                }); 
            _dtoFaker = new Faker<Product>()
                .RuleFor(p => p.Id, 0)
                .RuleFor(p => p.Name, f => f.Commerce.Product())
                .RuleFor(p => p.UnitPrice, f => f.Random.Int(200, 20000))
                .RuleFor(p => p.ShipmentRegion,
                    f => f.PickRandom<Dal.Entities.ShipmentRegion>())
                .RuleFor(p => p.CategoryId, 1)
                .RuleFor(p => p.RowVersion, f => f.Random.Bytes(5));
            _serializerOptions = appFactory.SerializerOptions;
            _testOutput = output;
            output.WriteLine("ProductControllerTests ctor");

        }

        public class Post : ProductControllerTests
        {
            public Post(CustomWebApplicationFactory appFactory, ITestOutputHelper output)
                : base(appFactory, output)
            {
                
            }

            [Fact]
            public async Task Should_Succeded_With_Created()
            {
                // Arrange
                _appFactory.Server.PreserveExecutionContext = true;
                using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var client = _appFactory.CreateClient();
                var dto = _dtoFaker.Generate();
                // Act
                var response = await client.PostAsJsonAsync("/api/products", dto, _serializerOptions);
                var p = await response.Content.ReadFromJsonAsync<Product>(_serializerOptions);
                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.Created);
                response.Headers.Location
                    .Should().Be(
                        new Uri(_appFactory.Server.BaseAddress, $"/api/Products/{p.Id}")
                    );
                p.Should().BeEquivalentTo(
                    dto,
                    opt => opt.Excluding(x => x.Category)
                        .Excluding(x => x.Orders)
                        .Excluding(x => x.Id)
                        .Excluding(x => x.RowVersion));
                p.Category.Should().NotBeNull();
                p.Category.Id.Should().Be(dto.CategoryId);
                p.Orders.Should().BeEmpty();
                p.Id.Should().BeGreaterThan(0);
                p.RowVersion.Should().NotBeEmpty();
            }

            [Theory]
            [InlineData("", "Product name is required.")]
            [InlineData(null, "Product name is required.")]
            public async Task Should_Fail_When_Name_Is_Invalid(string name, string expectedError)
            {
                // Arrange
                _appFactory.Server.PreserveExecutionContext = true;
                using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                var client = _appFactory.CreateClient();
                var dto = _dtoFaker.RuleFor(x => x.Name, name).Generate();
                // Act
                var response = await client.PostAsJsonAsync("/api/products", dto, _serializerOptions);
                var p = await response.Content
                    .ReadFromJsonAsync<ValidationProblemDetails>(_serializerOptions);
                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
                p.Status.Should().Be(400);
                p.Errors.Should().HaveCount(1);
                p.Errors.Should().ContainKey(nameof(Product.Name));
                p.Errors[nameof(Product.Name)].Should().ContainSingle(expectedError);
            }
        }
    }
}