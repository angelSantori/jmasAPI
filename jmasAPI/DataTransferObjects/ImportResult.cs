namespace jmasAPI.DataTransferObjects
{
    public class ImportResult
    {
        public int Imported { get; set; }
        public int Updated { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
