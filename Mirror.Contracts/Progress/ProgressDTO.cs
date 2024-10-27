using Mirror.Contracts.ProgressValue;

namespace Mirror.Contracts.Progress
{
    public class ProgressDTO
    {
        public string Description { get; set; } = null!;

        public DateTime Date { get; set; }

        public List<ProgressValueDTO> ProgressValue { get; set; } = [];
    }
}
