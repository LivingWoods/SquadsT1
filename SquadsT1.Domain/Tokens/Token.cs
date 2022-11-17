using SquadsT1.Domain.Common;

namespace SquadsT1.Domain.Tokens;

public class Token : Entity
{
    public bool IsUsed { get; private set; }
    public DateTime? UsedOn { get; private set; }
    public Payment? Payment { get; private set; }

    /// <summary>
    /// Returns wether or not the token has been paid
    /// </summary>
    public bool IsPaid => Payment is not null;

    /// <summary>
    /// EF constructor
    /// </summary>
    private Token() { }
    /// <summary>
    /// Validates and creates a new token
    /// </summary>
    /// <param name="payment">Wether or not the token has already been paid for</param>
    public Token(Payment? payment)
    {
        IsUsed = false;
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

    /// <summary>
    /// Sets the IsUsed property to true
    /// </summary>
    public void UseToken()
    {
        if (!IsUsed)
        {
            IsUsed = true;
            UsedOn = DateTime.UtcNow;
        }
    }
}
