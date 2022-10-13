using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalkController(IWalkRepository walkRepository,IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            //Fetch Data from Database- Domain Walks
            var walksDomain= await walkRepository.GetAllAsynch();

            //Convert Domain Walks to DTO Walks
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walksDomain);

            //Return Response
            return Ok(walksDTO);           
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalks")]
        public async Task<IActionResult> GetWalks(Guid id)
        {
            //Get Walk Domain from Database
            var walkDomain =await walkRepository.GetAsynch(id);

            //Convert Doamin To DTO
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);

            //Return Response
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalk(Models.DTO.WalkRequest addwalk)
        {
            //convert DTO to Domain
            var walkDomain = new Models.Domain.Walk()
            {
                Name = addwalk.Name,
                Length = addwalk.Length,
                RegionId = addwalk.RegionId,
                WalkDifficultyId = addwalk.WalkDifficultyId
            };

            //Add using Repository
            walkDomain= await walkRepository.AddWalkAsynch(walkDomain);

            //Convert Domain to DTO
            var walkDTO = new Models.DTO.Walk()
            {
                Id=walkDomain.Id,
                Name = walkDomain.Name,
                Length = walkDomain.Length,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            //Return Response
            return CreatedAtAction(nameof(GetWalks), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, [FromBody] Models.DTO.Walk updatewalk)
        {
            //Convert DTO to Domain
            var walkDomain = new Models.Domain.Walk()
            {
                Name = updatewalk.Name,
                Length = updatewalk.Length,
                RegionId = updatewalk.RegionId,
                WalkDifficultyId = updatewalk.WalkDifficultyId
            };

            //Update using Repository
            walkDomain = await walkRepository.UpdateWalkAsynch(id, walkDomain);

            //if Null NotFound
            if(walkDomain == null)
                return NotFound();

            //Convert Domain to DTO
            var walkDTO = new Models.DTO.Walk()
            {
                Id = walkDomain.Id,
                Name = walkDomain.Name,
                Length = walkDomain.Length,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };

            //Return Response
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            //Dlelete using Repository
            var walkDomain= await walkRepository.DeleteAsynch(id);

            //if Null NotFound
            if (walkDomain == null)
                return NotFound();

            //Convert Domain to DTO
            var walkDTO= mapper.Map<Models.DTO.Walk>(walkDomain);

            //Return Response
            return Ok(walkDTO);                        
        }
    }
}
