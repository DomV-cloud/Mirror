using Mirror.Contracts.ProgressValue;

namespace Mirror.Contracts.Progress
{
    public class ProgressDTO
    {
        public string Description { get; set; } = null!;

        public string ProgressColumnHead { get; set; } = null!;
        
        public List<ProgressValueDTO> ProgressValue { get; set; } = [];
    }
}
