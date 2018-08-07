namespace FastFood.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        //TODO int or double?
        public int Quantity { get; set; }
    }
}
