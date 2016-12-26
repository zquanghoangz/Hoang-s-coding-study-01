using System.Linq;

namespace BillingSystem.Tests
{
    public class BillingProcessor
    {
        public const int MAX_FAILURES = 3;
        private readonly ICustomerRepository _repo;
        private readonly ICreditCardCharger _charger;

        public BillingProcessor(ICustomerRepository repo, ICreditCardCharger charger)
        {
            _repo = repo;
            _charger = charger;
        }

        public void ProcessMonth(int year, int month)
        {
            var customer = _repo.Customers.Single();
            if (NeedsBilling(year, month, customer))
            {
                bool charged = _charger.ChargeCustomer(customer);

                if (!charged && ++customer.PaymentFailures >= MAX_FAILURES)
                {
                    customer.Subscribed = false;
                }

            }

        }

        private static bool NeedsBilling(int year, int month, Customer customer)
        {
            if (customer.Subscription != null)
            {

            }
            return customer.Subscribed &&
                            customer.PaidThroughMonth < month &&
                            customer.PaidThroughYear <= year;
        }
    }
}