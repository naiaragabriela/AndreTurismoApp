using AndreTurismoApp.ClientService.Controllers;
using AndreTurismoApp.ClientService.Data;
using AndreTurismoApp.ClientService.Models;
using AndreTurismoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AndreTurismoApp.Teste
{
    public class UnitTestClient
    {
        private DbContextOptions<AndreTurismoAppClientServiceContext>? options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppClientServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;


            using AndreTurismoAppClientServiceContext context = new(options);
            _ = context.Client.Add(new Customer
            {
                Id = 1,
                Name = "Simone",
                Phone = "992228974",
                Address = new()
                {
                    Id = 1,
                    Street = "Rua Paulo Chioda",
                    PostalCode = "14890238",
                    Number = 235,
                    Neighborhood = "Jardim Morumbi",
                    Complement = " ",
                    CityId = 3,
                    City = new City()
                    {
                        Id = 3,
                        Name = "Jaboticabal"
                    }

                }
            });

            _ = context.Client.Add(new Customer
            {
                Id = 2,
                Name = "Naiara",
                Phone = "992226574",
                Address = new()
                {
                    Id = 2,
                    Street = "Rua Alessio Santini",
                    PostalCode = "14804021",
                    Number = 260,
                    Neighborhood = "Jardim Paraíso",
                    Complement = " ",
                    CityId = 2,
                    City = new City()
                    {
                        Id = 2,
                        Name = "Araraquara"
                    }
                }
            });

            _ = context.SaveChanges();
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_GetClientById()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using AndreTurismoAppClientServiceContext context = new(options);
            int CustomerId = 2;
            CustomersController clientController = new(context);
            ActionResult<Customer> client = await clientController.GetCustomerById(CustomerId);
            Assert.Equal(2, client.Value?.Id);
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_PostClient()
        {
            InitializeDataBase();

            ClientPostRequestDTO client = new()
            {
                Name = "Ana",
                Phone = "33845678",
                AddressId = 2,
            };

            // Use a clean instance of the context to run the test
            using AndreTurismoAppClientServiceContext context = new(options);
            CustomersController clientController = new(context);
            ActionResult<Customer> response = await clientController.PostCustomer(client);
            CreatedAtActionResult? result = response.Result as CreatedAtActionResult;
            Assert.NotNull(result?.Value);
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_PutClient()
        {
            InitializeDataBase();

            ClientPutRequestDTO client = new()
            {
                Name = "Gustavo",
                Phone = "12345678",
            };

            // Use a clean instance of the context to run the test
            using AndreTurismoAppClientServiceContext context = new(options);
            CustomersController clientController = new(context);
            IActionResult response = await clientController.PutCustomer(3, client);
            NoContentResult? result = response as NoContentResult;
            Assert.NotNull(result);
        }

        [Fact]

        public async void ShouldBe_ReturnSucess_WhenCall_DeleteClient()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using AndreTurismoAppClientServiceContext context = new(options);
            CustomersController clientController = new(context);
            IActionResult response = await clientController.DeleteCustomer(2);
            NoContentResult? result = response as NoContentResult;
            Assert.NotNull(result);
        }
    }

}
