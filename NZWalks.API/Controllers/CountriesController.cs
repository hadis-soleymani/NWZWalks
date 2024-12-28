using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public CountriesController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var countriesDomainModel = dbContext.Countries.ToList();

            var response = new List<CountryDto>();
            foreach (var countriesDomain in countriesDomainModel)
            {
                response.Add(new CountryDto
                {
                    Id = countriesDomain.Id,
                    Name = countriesDomain.Name,
                });
            }
            return Ok(response);
        }
    }
}
