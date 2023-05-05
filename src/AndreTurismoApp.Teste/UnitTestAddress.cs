using AndreTurismoApp.AddressService.Controllers;
using AndreTurismoApp.AddressService.Data;
using AndreTurismoApp.AddressService.Models;
using AndreTurismoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AndreTurismoApp.Teste
{
    public class UnitTestAddress
    {
        private DbContextOptions<AndreTurismoAppAddressServiceContext>? options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppAddressServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using AndreTurismoAppAddressServiceContext context = new(options);
            _ = context.Address.Add(new Address
            {
                Id = 1,
                Street = "Avenida Augusto Ferreira",
                PostalCode = "15991528",
                Number = 260,
                Neighborhood = "Nova Cidade",
                Complement = " ",
                CityId = 1,
                City = new City() { Id = 1, Name = "Mat�o" }
            });

            _ = context.Address.Add(new Address
            {
                Id = 2,
                Street = "Rua Alessio Santini",
                PostalCode = "14804021",
                Number = 260,
                Neighborhood = "Jardim Para�so",
                Complement = " ",
                CityId = 2,
                City = new City() { Id = 2, Name = "Araraquara" }
            });
            _ = context.Address.Add(new Address
            {
                Id = 3,
                Street = "Rua Paulo Chioda",
                PostalCode = "14890238",
                Number = 235,
                Neighborhood = "Jardim Morumbi",
                Complement = " ",
                CityId = 3,
                City = new City() { Id = 3, Name = "Jaboticabal" }
            });
            _ = context.SaveChanges();
        }

        [Theory]
        [InlineData("Araraquara")]
        [InlineData("Mat�o")]
        public async Task ShouldBe_ReturnSucess_WhenCall_GetAddressByCity(string city)
        {

            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using AndreTurismoAppAddressServiceContext context = new(options);
            string postalCode = string.Empty;
            string neighborhood = string.Empty;

            AddressesController addressController = new(context);
            ActionResult<IEnumerable<Address>> address = await addressController.GetAddress(postalCode, city, neighborhood);

            Assert.Equal(1, address.Value?.Count());
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_GetAddressById()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using AndreTurismoAppAddressServiceContext context = new(options);
            int CustomerId = 2;
            AddressesController clientController = new(context);
            ActionResult<Address> client = await clientController.GetAddress(CustomerId);
            Assert.Equal(2, client.Value?.Id);
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_PostAddress()
        {
            InitializeDataBase();

            AddressPostRequestDTO address = new()
            {

                Number = 20,
                PostalCode = "14804300",
                Complement = " ",
                CityId = 10
            };

            // Use a clean instance of the context to run the test
            using AndreTurismoAppAddressServiceContext context = new(options);
            AddressesController addressController = new(context);
            ActionResult<Address> response = await addressController.PostAddress(address);
            CreatedAtActionResult? result = response.Result as CreatedAtActionResult;
            Assert.NotNull(result?.Value);
        }


        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_PutAddress()
        {
            InitializeDataBase();

            AddressPutRequestDTO address = new()
            {
                Number = 10,
                Complement = " ",
            };

            // Use a clean instance of the context to run the test
            using AndreTurismoAppAddressServiceContext context = new(options);
            AddressesController addressController = new(context);
            ActionResult response = await addressController.PutAddress(3, address);
            NoContentResult? result = response as NoContentResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_DeleteAddress()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using AndreTurismoAppAddressServiceContext context = new(options);
            AddressesController addressController = new(context);
            ActionResult response = await addressController.DeleteAddress(2);
            NoContentResult? result = response as NoContentResult;
            Assert.NotNull(result);
        }
    }
}