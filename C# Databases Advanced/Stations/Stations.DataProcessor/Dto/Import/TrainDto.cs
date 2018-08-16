namespace Stations.DataProcessor.Dto.Import
{
    using System.ComponentModel.DataAnnotations;
    using Models.Enums;

    public class TrainDto
    {
        [Required]
        [MaxLength(10)]
        //unique
        public string TrainNumber { get; set; }

        public string Type { get; set; } = TrainType.HighSpeed.ToString();

        public SeatDto[] Seats { get; set; } = new SeatDto[0];
    }
}
