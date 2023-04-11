namespace SMTstock.Entities.Models
{
    public class Merchant:BaseEntity
    {
        public Merchant()
        {
            Orders = new HashSet<Order>();
        }
        public string MerchantName { get; set; }
        public virtual ICollection<Order> Orders { get;set; }
    }
}
