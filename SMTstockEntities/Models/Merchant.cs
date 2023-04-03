namespace SMTstock.Entities.Models
{
    public class Merchant
    {
        public Merchant()
        {
            Orders = new HashSet<Order>();
        }
        public int Id { get; set; }
        public string MerchantName { get; set; }
        public virtual ICollection<Order> Orders { get;set; }
    }
}
