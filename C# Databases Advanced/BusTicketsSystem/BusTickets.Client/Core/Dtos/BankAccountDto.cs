namespace BusTickets.Client.Core.Dtos
{
    public class BankAccountDto
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public CustomerDto Customer { get; set; }

        public decimal Balance { get; set; }
    }
}
