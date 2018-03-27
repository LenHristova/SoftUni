namespace P01.Stream_Progress
{
    public class StreamProgressInfo
    {
        private readonly IStreamable istreamable;

        // If we want to stream a music file, we can't
        public StreamProgressInfo(IStreamable istreamable)
        {
            this.istreamable = istreamable;
        }

        public int CalculateCurrentPercent()
        {
            return (this.istreamable.BytesSent * 100) / this.istreamable.Length;
        }
    }
}
