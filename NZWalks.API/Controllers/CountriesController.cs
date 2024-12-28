using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.V1.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]

    public class CountriesController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public CountriesController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public IActionResult GetV1()
        {
            var countriesDomainModel = dbContext.Countries.ToList();

            var response = new List<CountryDtoV1>();
            foreach (var countriesDomain in countriesDomainModel)
            {
                response.Add(new CountryDtoV1
                {
                    Id = countriesDomain.Id,
                    Name = countriesDomain.Name,
                });
            }
            return Ok(response);
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        public IActionResult GetV2()
        {
            var countriesDomainModel = dbContext.Countries.ToList();

            var response = new List<CountryDtoV2>();
            foreach (var countriesDomain in countriesDomainModel)
            {
                response.Add(new CountryDtoV2
                {
                    Id = countriesDomain.Id,
                    CountryName = countriesDomain.Name,
                });
            }
            return Ok(response);
        }
    }
}
