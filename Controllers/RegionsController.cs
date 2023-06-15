using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // Get all regions
        // GET: localhost:portnumber/api/regions
        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll()
        {
            // Get Data from DB - Domain Models
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map Domain Models to DTOs (Data Transfer Objects)
            var regionsDto = mapper.Map<List<RegionDTO>>(regionsDomain);

            // Return DTO back to client
            return Ok(regionsDto);
        }

        // GET SINGLE REGION
        // GET: localhost:portnumber/api/regions/{id}
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var regionDomain = await regionRepository.GetByIDAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDTO>(regionDomain);

            return Ok(regionDto);
        }

        // POST to create a new region
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateAsync([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            var createdRegion = await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDTO>(createdRegion);

            return Ok(regionDto); // Manually return a 200 OK status code
        }


        // Update Region
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            var updatedRegion = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (updatedRegion == null)
            {
                return NotFound();
            }

            var updatedRegionDto = mapper.Map<RegionDTO>(updatedRegion);

            return Ok(updatedRegionDto);
        }

        // Delete Region
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deletedRegion = await regionRepository.DeleteAsync(id);

            if (deletedRegion == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDTO>(deletedRegion));
        }
    }
}
