namespace WebApplication1.Models.DTO
{
    public class WalkRequest
    {
        public string Name { get; set; }

        public double Length { get; set; }

        public Guid RegionId { get; set; }

        public Guid WalkDifficultyId { get; set; }
    }
}
