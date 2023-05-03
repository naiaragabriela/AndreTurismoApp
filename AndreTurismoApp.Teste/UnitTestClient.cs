using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AndreTurismoApp.AddressService.Data;
using AndreTurismoApp.ClientService.Data;
using AndreTurismoApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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

                    Name = "Simone",
                    Phone = "992228974",
                    Address = new()
                    {
                        Street = "Rua Paulo Chioda",
                        PostalCode = "14890238",
                        Number = 235,
                        Neighborhood = "Jardim Morumbi",
                        Complement = " ",
                        CityId = 3,
                        City = new City()
                        {
                            Id = 3,
                            Name = "Jabotical"
                        }

                    }
                });
                context.Client.Add(new Client
                {

                    Name = "Naiara",
                    Phone = "992226574",
                    Address = new()
                    {
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


            }

        }
    }
}
