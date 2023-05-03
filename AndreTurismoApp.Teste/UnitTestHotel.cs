using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismo.App.HotelService.Controllers;
using AndreTurismo.App.HotelService.Data;
using AndreTurismoApp.ClientService.Controllers;
using AndreTurismoApp.ClientService.Data;
using AndreTurismoApp.ClientService.Models;
using AndreTurismoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AndreTurismoApp.Teste
{
    public class UnitTestHotel
    {
        private DbContextOptions<AndreTurismoAppHotelServiceContext>? options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppHotelServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;


            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                context.Hotel.Add(new Hotel
                {
                    Id = 1,
                    Name = "Hotel Ibis",
                    Cost = 200,
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

                context.Hotel.Add(new Hotel
                {
                    Id = 2,
                    Name = "Hotel Flórida",
                    Cost = 250,
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
        public async void ShouldBe_ReturnSucess_WhenCall_GetHotelById()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppHotelServiceContext(options))
            {
                var hotelId = 2;
                var hotelController = new HotelsController(context);
                var hotel = await hotelController.GetHotel(hotelId);
                Assert.Equal(2, hotel.Value?.Id);
            }
        }
        /*
        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_PostHotel()
        {
            InitializeDataBase();

            HotelPostRequestDTO hotel = new()
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
        */
        
    }
}
