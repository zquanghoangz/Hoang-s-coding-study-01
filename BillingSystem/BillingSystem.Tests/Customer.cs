namespace BillingSystem.Tests
{
    public class Customer
    {
        public bool Subscribed { get; set; }
        public int PaidThroughYear { get; set; }
        public int PaidThroughMonth { get; set; }

        public int PaymentFailures { get; set; }
        public Subscription Subscription { get; set; }
    }
}