using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        #region Init
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        #endregion

        // Get all Regions
        // GET:  https://localhost:portnumber/api/regions
        #region GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await regionRepository.GetAllAsync();
            var regionsDto = mapper.Map<List<RegionDto>>(result);

            return Ok(regionsDto);
        }
        #endregion

        // Get single Region (Get region by ID)
        // GET:  https://localhost:portnumber/api/regions/{id}
        #region GetById

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
        #endregion

        // Create Region
        // POST:  https://localhost:portnumber/api/regions
        #region Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //map or convert dto to domain model
            var regionDomainModel = new Region
            {
                Name = addRegionRequestDto.Name,
                Code = addRegionRequestDto.Code,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //map domain modle back to DTO
            var regionDto = new Region
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }
        #endregion

        // Update Region
        // PUT:  https://localhost:portnumber/api/regions/{id}
        #region Update
        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //map Dto to domain model
            var regionDomainModel = new Region
            {
                Name = updateRegionRequestDto.Name,
                Code = updateRegionRequestDto.Code,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl,
            };

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //convert domain model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }
        #endregion

        // Delete Region
        // DELETE :  https://localhost:portnumber/api/regions/{id}
        #region Delete
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //convert domain model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }
        #endregion
    }
}
