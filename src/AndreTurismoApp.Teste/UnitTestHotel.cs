﻿using AndreTurismo.App.HotelService.Controllers;
using AndreTurismo.App.HotelService.Data;
using AndreTurismo.App.HotelService.Models;
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


            using AndreTurismoAppHotelServiceContext context = new(options);
            _ = context.Hotel.Add(new Hotel
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

            _ = context.Hotel.Add(new Hotel
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

            _ = context.SaveChanges();
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_GetHotelById()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using AndreTurismoAppHotelServiceContext context = new(options);
            int hotelId = 2;
            HotelsController hotelController = new(context);
            ActionResult<Hotel> hotel = await hotelController.GetHotel(hotelId);
            Assert.Equal(2, hotel.Value?.Id);
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_PostHotel()
        {
            InitializeDataBase();

            HotelPostRequestDTO hotel = new()
            {
                Id = 3,
                Name = "Hotel Florença",
                Cost = 200,
                AddressId = 1,
            };

            // Use a clean instance of the context to run the test
            using AndreTurismoAppHotelServiceContext context = new(options);
            HotelsController hotelController = new(context);
            ActionResult<Hotel> response = await hotelController.PostHotel(hotel);
            CreatedAtActionResult? result = response.Result as CreatedAtActionResult;
            Assert.NotNull(result?.Value);
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_PutHotel()
        {
            InitializeDataBase();

            HotelPutRequestDTO hotel = new()
            {
                Id = 1,
                Name = "Hotel Ibis",
                Cost = 150,
            };

            // Use a clean instance of the context to run the test
            using AndreTurismoAppHotelServiceContext context = new(options);
            HotelsController hotelController = new(context);
            IActionResult response = await hotelController.PutHotel(1, hotel);
            NoContentResult? result = response as NoContentResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_DeleteHotel()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using AndreTurismoAppHotelServiceContext context = new(options);
            HotelsController hotelController = new(context);
            IActionResult response = await hotelController.DeleteHotel(2);
            NoContentResult? result = response as NoContentResult;
            Assert.NotNull(result);
        }
    }
}
