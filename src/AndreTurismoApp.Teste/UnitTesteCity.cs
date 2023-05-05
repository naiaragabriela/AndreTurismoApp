using AndreTurismoApp.CityService.Controllers;
using AndreTurismoApp.CityService.Data;
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
            using AndreTurismoAppCityServiceContext context = new(options);
            _ = context.City.Add(new City
            {
                Id = 1,
                Name = "Matão"
            });

            _ = context.City.Add(new City
            {
                Id = 2,
                Name = "Araraquara"
            });

            _ = context.City.Add(new City
            {
                Id = 3,
                Name = "Jaboticabal"
            });

            _ = context.SaveChanges();
        }

        [Theory]
        [InlineData("Araraquara")]
        [InlineData("Matão")]
        public async Task ShouldBe_ReturnSucess_WhenCall_GetCity(string city)
        {

            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using AndreTurismoAppCityServiceContext context = new(options);
            CitiesController cityController = new(context);
            ActionResult<IEnumerable<City>> address = await cityController.GetCity(city);

            Assert.Equal(1, address.Value?.Count());
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_GetAddressById()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using AndreTurismoAppCityServiceContext context = new(options);
            int cityId = 2;
            CitiesController cityController = new(context);
            ActionResult<City> city = await cityController.GetCity(cityId);
            Assert.Equal(2, city.Value?.Id);
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
            using AndreTurismoAppCityServiceContext context = new(options);
            CitiesController cityController = new(context);
            ActionResult<City> response = await cityController.PostCity(city);
            CreatedAtActionResult? result = response.Result as CreatedAtActionResult;
            Assert.NotNull(result?.Value);
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
            using AndreTurismoAppCityServiceContext context = new(options);
            CitiesController cityController = new(context);
            IActionResult response = await cityController.PutCity(4, city);
            NoContentResult? result = response as NoContentResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldBe_ReturnSucess_WhenCall_DeleteCity()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using AndreTurismoAppCityServiceContext context = new(options);
            CitiesController cityController = new(context);
            IActionResult response = await cityController.DeleteCity(2);
            NoContentResult? result = response as NoContentResult;
            Assert.NotNull(result);
        }

    }
}
