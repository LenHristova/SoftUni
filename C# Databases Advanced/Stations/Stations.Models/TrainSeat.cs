namespace Stations.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TrainSeat
    {
        //•	Id – integer, Primary Key
        //•	TrainId – integer(required)
        //•	Train – train whose seats will be described(required)
        //•	SeatingClassId – integer(required)
        //•	SeatingClass – class of the seats(required)
        //•	Quantity – how many seats of given class total for the given train(required, non-negative)

        public int Id { get; set; }

        [Required]
        public int TrainId { get; set; }
        [Required]
        public virtual Train Train { get; set; }

        [Required]
        public int SeatingClassId { get; set; }
        [Required]
        public virtual SeatingClass SeatingClass { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
