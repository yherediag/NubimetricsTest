using API.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;

namespace API.Tests
{
    public class BasePruebas
    {
        protected static NubimetricsExampleContext ConstruirContext(string nombreDB)
        {
            var opciones = new DbContextOptionsBuilder<NubimetricsExampleContext>()
                .UseInMemoryDatabase(nombreDB).Options;

            var dbContext = new NubimetricsExampleContext(opciones);
            return dbContext;
        }

        protected static IMapper ConfigurarAutoMapper()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMappingHelper());
            });

            return config.CreateMapper();
        }

        protected static IConfiguration ConstruirConfiguration()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"UnauthorizedCountries", "BR;CO"}
            };

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

            return config;
        }

        protected static IHttpGetService ConstruirHttpHelper()
        {
            var httpClient = new HttpClient();
            var httpHelper = new HttpHelper(httpClient);

            return httpHelper;
        }
    }
}