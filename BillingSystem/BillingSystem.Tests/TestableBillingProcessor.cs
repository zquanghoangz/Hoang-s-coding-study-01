using Moq;

namespace BillingSystem.Tests
{
    public class TestableBillingProcessor : BillingProcessor
    {
        public Mock<ICreditCardCharger> MockCharger { get; set; }
        public Mock<ICustomerRepository> MockRepo { get; set; }

        public TestableBillingProcessor(Mock<ICustomerRepository> mockRepo, Mock<ICreditCardCharger> mockCharger)
            : base(mockRepo.Object, mockCharger.Object)
        {
            MockRepo = mockRepo;
            MockCharger = mockCharger;
        }

        public static TestableBillingProcessor Create(params Customer[] customers)
        {
            var repo = new Mock<ICustomerRepository>();
            repo.Setup(r => r.Customers)
                .Returns(customers);

            return new TestableBillingProcessor(
                repo,
                new Mock<ICreditCardCharger>());
        }
    }
}