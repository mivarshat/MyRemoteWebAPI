using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDomain = await walkDifficultyRepository.GetAll();

            //Convert Domain to DTO
          //  var walkDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDomain);

            return Ok(walkDomain);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficulties")]
        public async Task<IActionResult> GetWalkDifficulties(Guid id)
        {
            var walkD = await walkDifficultyRepository.Get(id);

            if (walkD == null)
                return NotFound();

            //Convert Domain to DTO
            var walkDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkD);

            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficulties(Models.DTO.WalkDifficultyRequest addwalk)
        {
            //Convert DTO to Domain
            var walkDomain = new Models.Domain.WalkDifficulty()
            {
                Code = addwalk.Code
            };

            //Add using Rpository
            walkDomain = await walkDifficultyRepository.Add(walkDomain);

            var walkDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDomain);

            //return Response
            return CreatedAtAction(nameof(GetWalkDifficulties), new {id = walkDTO.Id}, walkDTO);
        }
    }
}
