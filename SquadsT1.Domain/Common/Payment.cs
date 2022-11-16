namespace SquadsT1.Domain.Common;

public class Payment
{
    public DateTime PaidOn { get; set; }

	public Payment()
	{
		PaidOn = DateTime.UtcNow;
	}
}
