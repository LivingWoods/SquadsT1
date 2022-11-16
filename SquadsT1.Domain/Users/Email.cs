using Ardalis.GuardClauses;

namespace SquadsT1.Domain.Users;

public class Email
{
	private string _validEmailRegexPattern = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
	public string EmailAddress { get; }

	public Email(string email)
	{
		email = email.Trim();

		Guard.Against.NullOrEmpty(email, nameof(email));
		Guard.Against.NullOrWhiteSpace(email, nameof(email));
		Guard.Against.InvalidFormat(email, nameof(email), _validEmailRegexPattern);

		EmailAddress = email.ToLower();

	}
}
