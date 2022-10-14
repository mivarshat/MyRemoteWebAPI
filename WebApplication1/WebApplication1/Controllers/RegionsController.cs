using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Domain;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsynch()
        {
            var regions = await regionRepository.GetAllAsynch();

            //return DTO regions instead of Domain regions
            //var regionsDTOList = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{
            //    var regionsDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population
            //    };
            //    regionsDTOList.Add(regionsDTO);
            //});

            var regionsDTOList = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTOList);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetAsynch")]
        public async Task<IActionResult> GetAsynch(Guid id)
        {
            var region = await regionRepository.GetAsynch(id);
            if(region == null)
            {
                return NotFound();
            }
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsynch(Models.DTO.RegionRequest regionRequest)
        {
            //Validate the Request
            if (!ValidateAddAsynch(regionRequest))
                return BadRequest(ModelState);

            //Request (DTO) to Domain Model
            var region = new Models.Domain.Region()
            {
                Code = regionRequest.Code,
                Area = regionRequest.Area,
                Lat = regionRequest.Lat,
                Long = regionRequest.Long,
                Name = regionRequest.Name,
                Population = regionRequest.Population
            };

            //Pass details to Repository
            region=  await regionRepository.AddAsynch(region);

            //Convert back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetAsynch), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsynch(Guid id)
        {
            //Get region from database
           var region=  await regionRepository.DeleteAsynch(id);

            //If null NotFound
            if(region==null)
            return NotFound(id);

            //Convert response back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Long = region.Long,
                Lat = region.Lat,
                Area = region.Area,
                Population = region.Population
            };

            //return OK response
            return Ok(regionDTO);
        }

        [HttpPut] 
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsynch([FromRoute] Guid id,[FromBody] Models.DTO.RegionRequest regionUpdate)
        {
            //Convert DTO to Domail Model
            var region = new Models.Domain.Region()
            {
                Code = regionUpdate.Code,
                Area = regionUpdate.Area,
                Name = regionUpdate.Name,
                Lat = regionUpdate.Lat,
                Long = regionUpdate.Long,
                Population = regionUpdate.Population
            };

            //Update Region using Repository
            region= await regionRepository.UpdateAsynch(id, region);

            //If Null Not Found
            if (region == null)
                return NotFound();

            //Convert Domain back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                Code = region.Code,
                Area = region.Area,
                Name = region.Name,
                Lat = region.Lat,
                Long = region.Long,
            };

            //Return Ok Reposnse
            return Ok(regionDTO);
        }

        #region Private Methods

        private bool ValidateAddAsynch(Models.DTO.RegionRequest regionRequest)
        {
            if (regionRequest == null)
            {
                ModelState.AddModelError(nameof(regionRequest), $"Add Region Data is required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(regionRequest.Code))
            {
                ModelState.AddModelError(nameof(regionRequest.Code), $"{nameof(regionRequest.Code)} can not be null or empty or white space");
            }
            if (string.IsNullOrWhiteSpace(regionRequest.Name))
            {
                ModelState.AddModelError(nameof(regionRequest.Name), $"{nameof(regionRequest.Name)} can not be null or empty or white space");
            }
            if(regionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(regionRequest.Area), $"{nameof(regionRequest.Area)} can not be less than or equal to zero");
            }
            if (regionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(regionRequest.Lat), $"{nameof(regionRequest.Lat)} can not be less than or equal to zero");
            }
            if (regionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(regionRequest.Long), $"{nameof(regionRequest.Long)} can not be less than or equal to zero");
            }
            if (regionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(regionRequest.Population), $"{nameof(regionRequest.Population)} can not be less than to zero");
            }
            
            if(ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion
    }


}
