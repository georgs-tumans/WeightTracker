namespace WeightTrack.Data.Responses
{
    public class ProgressResponse
    {
        /// <summary>
        /// Current weight of the user (latest weight entry)
        /// </summary>
        public double CurrentWeight { get; set; }
        /// <summary>
        /// Target weight of the particular target entry
        /// </summary>
        public double TargetWeigth { get; set; }
        /// <summary>
        /// Target entry Id
        /// </summary>
        public int TargetId { get; set; }
        /// <summary>
        /// Target date
        /// </summary>
        public DateTime TargetDate { get; set; }
    }
}
