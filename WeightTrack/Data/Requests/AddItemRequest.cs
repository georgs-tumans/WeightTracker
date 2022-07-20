using System.ComponentModel.DataAnnotations;

namespace WeightTrack.Data.Requests
{
    public class AddItemRequest
    {

        /*Weight and date are nullable types because otherwise their value is automatically initialized to default values when nothing is passed in an incoming json.
        To get the automatically generated error message, they must be nullable and have the 'Required' tag 
        */

        /// <summary>
        /// Weight to enter
        /// </summary>
        [Required]
        public double? Weight { get; set; }
        /// <summary>
        /// Date of the weight measurement.Format: '2022-07-20'
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
        /// <summary>
        /// An optional note
        /// </summary>
        [StringLength(maximumLength: 2000)]
        [RegularExpression("^[a-zA-Z0-9](?:[a-zA-Z0-9.,'_ -]*[a-zA-Z0-9])?$", ErrorMessage = "Only letters and number allowed")]
        public string? Note { get; set; }
    }
}
