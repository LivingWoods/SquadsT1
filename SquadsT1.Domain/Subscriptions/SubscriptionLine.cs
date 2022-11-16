using Ardalis.GuardClauses;
using SquadsT1.Domain.Common;

namespace SquadsT1.Domain.Subscriptions;

public class SubscriptionLine
{
    public DateTime ValidFrom { get; }
    public DateTime ValidTill { get; }
    public Payment? Payment { get; private set; }

    /// <summary>
    /// Returns wether or not the token has been paid
    /// </summary>
    public bool IsPaid => Payment is not null;

    public SubscriptionLine(DateTime validFrom, DateTime validTill, Payment? payment)
    {
        ValidFrom = Guard.Against.OutOfRange(validFrom, nameof(validFrom), DateTime.UtcNow, DateTime.MaxValue);
        ValidTill = Guard.Against.OutOfRange(validTill, nameof(validTill), validFrom, DateTime.MaxValue);
        Payment = payment;
    }

    /// <summary>
    /// Adds a payment for the token
    /// </summary>
    /// <param name="payment">The payment for the token</param>
    public void AddPayment(Payment payment)
    {
        if (!IsPaid)
        {
            Payment = payment;
        }
    }
}
