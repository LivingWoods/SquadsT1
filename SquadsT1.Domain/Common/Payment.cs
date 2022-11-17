using Ardalis.GuardClauses;

namespace SquadsT1.Domain.Common;

public class Payment
{
    public DateTime PaidOn { get; }
	public double Amount { get; }

	public Payment(double amount)
	{
		Amount = Guard.Against.NegativeOrZero(amount, nameof(amount));
		PaidOn = DateTime.UtcNow;
	}
}
