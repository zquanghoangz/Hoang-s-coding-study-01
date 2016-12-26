namespace BillingSystem.Tests
{
    public interface ICreditCardCharger
    {
        bool ChargeCustomer(Customer customer);
    }
}