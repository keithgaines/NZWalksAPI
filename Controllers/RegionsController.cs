﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

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
        // GET : localhost:portnumber/api/regions
        [HttpGet]
        [Authorize(Roles = "Reader")]
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
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Reader")]
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

        // POST to create new region
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateAsync([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = regionDomainModel.Id }, regionDto);
        }

        // Update Region
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut("{id:guid}")]
        [ValidateModel]
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
