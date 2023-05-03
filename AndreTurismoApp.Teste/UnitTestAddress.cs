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
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                context.Address.Add(new Address
                {
                    Id = 1,
                    Street = "Avenida Augusto Ferreira",
                    PostalCode = "15991528",
                    Number = 260,
                    Neighborhood = "Nova Cidade",
                    Complement = " ",
                    CityId = 1,
                    City = new City() { Id = 1, Name = "Matão" }
                });

                context.Address.Add(new Address
                {
                    Id = 2,
                    Street = "Rua Alessio Santini",
                    PostalCode = "14804021",
                    Number = 260,
                    Neighborhood = "Jardim Paraíso",
                    Complement = " ",
                    CityId = 2,
                    City = new City() { Id = 2, Name = "Araraquara" }
                });
                context.Address.Add(new Address
                {
                    Id = 3,
                    Street = "Rua Paulo Chioda",
                    PostalCode = "14890238",
                    Number = 235,
                    Neighborhood = "Jardim Morumbi",
                    Complement = " ",
                    CityId = 3,
                    City = new City() { Id = 3, Name = "Jabotical" }
                });
                context.SaveChanges();
            }
        }

        [Theory]
        [InlineData("Araraquara")]
        [InlineData("Matão")]
        public async Task ShouldBe_ReturnSucess_WhenCall_GetAddressByCity(string city)
        {

            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                var postalCode = string.Empty;
                var neighborhood = string.Empty;

                var addressController = new AddressesController(context);
                var address = await addressController.GetAddress(postalCode, city, neighborhood);

                Assert.Equal(1, address.Value?.Count());
            }
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_GetAddressById()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                var clientId = 2;
                var clientController = new AddressesController(context);
                var client = await clientController.GetAddress(clientId);
                Assert.Equal(2, client.Value?.Id);
            }
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_PostAddress()
        {
            InitializeDataBase();

            AddressPostRequestDTO address = new ()
            {

                Number = 20,
                PostalCode = "14804300",
                Complement = " ",
                CityId = 10
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                var addressController = new AddressesController(context);
                var response = await addressController.PostAddress(address);
                var result = response.Result as CreatedAtActionResult;
                Assert.NotNull(result?.Value);
            }
        }


        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_PutAddress()
        {
            InitializeDataBase();

            AddressPutRequestDTO address = new ()
            {
                Number = 10,
                PostalCode = "14804300",
                Complement =  " ",
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                var addressController = new AddressesController(context);
                var response = await addressController.PutAddress(3, address);
                var result = response as NoContentResult;
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_DeleteAddress()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppAddressServiceContext(options))
            {
                var addressController = new AddressesController(context);
                var response = await addressController.DeleteAddress(2);
                var result = response as NoContentResult;
                Assert.NotNull(result);
            }
        }
    }
}