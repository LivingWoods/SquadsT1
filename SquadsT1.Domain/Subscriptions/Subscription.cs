using SquadsT1.Domain.Common;

namespace SquadsT1.Domain.Subscriptions;

public class Subscription
{
    private List<SubscriptionLine> _subscriptionLines = new();

    public bool IsCanceled { get; private set; }

    /// <summary>
    /// Returns the latest subscription line
    /// </summary>
    public SubscriptionLine GetLatestSubscriptionLine => _subscriptionLines.Last();

    /// <summary>
    /// Validates and creates a new subscription
    /// </summary>
    public Subscription()
    {
        IsCanceled = false;
    }

    /// <summary>
    /// Cancels the subscription
    /// </summary>
    public void CancelSubscription()
    {
        if (!IsCanceled)
        {
            IsCanceled = true;
        }
    }

    /// <summary>
    /// Reactivates the subscription
    /// </summary>
    public void ReactivateSubscription()
    {
        if (IsCanceled)
        {
            IsCanceled = false;
        }
    }

    /// <summary>
    /// Adds a new subscription line
    /// </summary>
    /// <param name="validFrom"></param>
    /// <param name="validTill"></param>
    /// <param name="payment"></param>
    public void RenewSubscription(DateTime validFrom, Payment? payment)
    {
        _subscriptionLines.Add(new SubscriptionLine(validFrom, payment));
    }
}
