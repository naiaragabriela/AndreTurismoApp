using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AndreTurismoApp.AddressService.Controllers;
using AndreTurismoApp.AddressService.Data;
using AndreTurismoApp.AddressService.Models;
using AndreTurismoApp.ClientService.Controllers;
using AndreTurismoApp.ClientService.Data;
using AndreTurismoApp.ClientService.Models;
using AndreTurismoApp.Controllers;
using AndreTurismoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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


            using (var context = new AndreTurismoAppClientServiceContext(options))
            {
                context.Client.Add(new Client
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

                context.Client.Add(new Client
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

                context.SaveChanges();
            }
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_GetClientById()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppClientServiceContext(options))
            {
                var clientId = 2;
                var clientController = new ClientsController(context);
                var client = await clientController.GetClient(clientId);
                Assert.Equal(2, client.Value?.Id);
            }
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
            using (var context = new AndreTurismoAppClientServiceContext(options))
            {
                var clientController = new ClientsController(context);
                var response = await clientController.PostClient(client);
                var result = response.Result as CreatedAtActionResult;
                Assert.NotNull(result?.Value);
            }
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
            using (var context = new AndreTurismoAppClientServiceContext(options))
            {
                var clientController = new ClientsController(context);
                var response = await clientController.PutClient(3, client);
                var result = response as NoContentResult;
                Assert.NotNull(result);
            }
        }

        [Fact]

        public async void ShouldBe_ReturnSucess_WhenCall_DeleteClient()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppClientServiceContext(options))
            {
                var clientController = new ClientsController(context);
                var response = await clientController.DeleteClient(2);
                var result = response as NoContentResult;
                Assert.NotNull(result);
            }
        }
    }

}
