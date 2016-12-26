using System.Collections.Generic;
using BillingSystem.Tests;
using Moq;
using Xunit;


public sealed class BillingProcessorTests
{
    public class NoSubscription
    {
        [Fact]
        public void CustomerWhoDoesNotHaveSubscriptionDoesNotGetCharged()
        {
            var processor = TestableBillingProcessor.Create(new Customer());

            processor.ProcessMonth(2011, 8);

            processor.MockCharger.Verify(c => c.ChargeCustomer(new Customer()), Times.Never);
        }
    }

    public class Monthly
    {
        [Fact]
        public void CustomerWithSubscriptionThatIsExpiredGetsCharged()
        {
            var subscription = new MonthlySubscription();
            var customer = new Customer { Subscription = subscription };
            var processor = TestableBillingProcessor.Create(customer);

            processor.ProcessMonth(2011, 8);

            processor.MockCharger.Verify(c => c.ChargeCustomer(customer), Times.Once());
        }
    }
    public class Annual { }





    [Fact]
    public void CustomerWithSubscriptionThatIsCurrentDoesNotGetCharged()
    {
        var customer = new Customer
        {
            Subscribed = true,
            PaidThroughYear = 2012,
            PaidThroughMonth = 1
        };
        var processor = TestableBillingProcessor.Create(customer);

        processor.ProcessMonth(2011, 8);

        processor.MockCharger.Verify(c => c.ChargeCustomer(customer), Times.Never());
    }

    [Fact]
    public void CustomerWhoIsSubscribedAndDueToPayButFailsOnceIsStillCurrent()
    {
        var customer = new Customer
        {
            Subscribed = true,
            PaidThroughYear = 2012,
            PaidThroughMonth = 1
        };
        var processor = TestableBillingProcessor.Create(customer);
        processor.MockCharger.Setup(c => c.ChargeCustomer(It.IsAny<Customer>()))
            .Returns(false);

        processor.ProcessMonth(2011, 8);

        //processor.MockCharger.Verify(c => c.ChargeCustomer(customer), Times.Never());
        Assert.True(customer.Subscribed);
    }
    [Fact]
    public void CustomerWhoIsSubscribedAndDueToPayButFailsThreeTimesIsNoLongerSubcribed()
    {
        var customer = new Customer
        {
            Subscribed = true,
            PaidThroughYear = 2011,
            PaidThroughMonth = 8
        };
        var processor = TestableBillingProcessor.Create(customer);
        processor.MockCharger.Setup(c => c.ChargeCustomer(It.IsAny<Customer>()))
            .Returns(false);

        for (int i = 0; i < BillingProcessor.MAX_FAILURES; i++)
        {
            processor.ProcessMonth(2011, 8);
        }

        //processor.MockCharger.Verify(c => c.ChargeCustomer(customer), Times.Never());
        Assert.True(customer.Subscribed);
    }

    //[Fact]
    //public void SuccessfulChargeOfSubscribed
}

namespace BillingSystem.Tests
{
    public abstract class Subscription
    {
        public abstract bool IsCurrent { get; }
        public abstract bool IsRecurring { get; }
        public abstract bool NeedsBilling(int year, int month);
    }

    public class AnnualSubscription : Subscription
    {
        public override bool IsCurrent { get; }

        public override bool IsRecurring
        {
            get
            {
                return false;

            }
        }

        public override bool NeedsBilling(int year, int month)
        {
            return true;
        }

    }

    public class MonthlySubscription : Subscription
    {
        public override bool IsCurrent { get; }

        public override bool IsRecurring
        {
            get
            {
                return true;

            }
        }

        public override bool NeedsBilling(int year, int month)
        {
            throw new System.NotImplementedException();
        }
    }
}
