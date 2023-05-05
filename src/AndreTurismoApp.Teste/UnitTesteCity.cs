using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.AddressService.Controllers;
using AndreTurismoApp.AddressService.Data;
using AndreTurismoApp.AddressService.Models;
using AndreTurismoApp.CityService.Controllers;
using AndreTurismoApp.CityService.Data;
using AndreTurismoApp.ClientService.Data;
using AndreTurismoApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AndreTurismoApp.Teste
{
    public class UnitTesteCity
    {
        private DbContextOptions<AndreTurismoAppCityServiceContext>? options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AndreTurismoAppCityServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                context.City.Add(new City
                {
                    Id = 1, 
                    Name = "Matão" 
                });

                context.City.Add(new City
                {
                    Id = 2,
                    Name = "Araraquara"
                });

                context.City.Add(new City
                {
                    Id = 3,
                    Name = "Jaboticabal"
                });

                context.SaveChanges();
            }
        }

        [Theory]
        [InlineData("Araraquara")]
        [InlineData("Matão")]
        public async Task ShouldBe_ReturnSucess_WhenCall_GetCity(string city)
        {

            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCityServiceContext(options))
            { 
                var cityController = new CitiesController(context);
                var address = await cityController.GetCity(city);

                Assert.Equal(1, address.Value?.Count());
            }
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_GetAddressById()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                var cityId = 2;
                var cityController = new CitiesController(context);
                var city = await cityController.GetCity(cityId);
                Assert.Equal(2, city.Value?.Id);
            }
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_PostAddress()
        {
            InitializeDataBase();

            City city = new()
            {

                Id = 4,
                Name = "São Carlos",
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                var cityController = new CitiesController(context);
                var response = await cityController.PostCity(city);
                var result = response.Result as CreatedAtActionResult;
                Assert.NotNull(result?.Value);
            }
        }


        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_PutCity()
        {
            InitializeDataBase();

            City city = new()
            {
                Id = 4,
                Name = "Botucatu"
            };

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                var cityController = new CitiesController(context);
                var response = await cityController.PutCity(4,city);
                var result = response as NoContentResult;
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_DeleteCity()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AndreTurismoAppCityServiceContext(options))
            {
                var cityController = new CitiesController(context);
                var response = await cityController.DeleteCity(2);
                var result = response as NoContentResult;
                Assert.NotNull(result);
            }
        }

    }
}
