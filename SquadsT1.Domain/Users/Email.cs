﻿using SquadsT1.Domain.Common;
using Ardalis.GuardClauses;
using System.Net.Mail;

namespace SquadsT1.Domain.Users;

public class Email : ValueObject
{
	public string Value { get; }

	/// <summary>
	/// EF constructor
	/// </summary>
	private Email() { }
    /// <summary>
    /// Validates and creates a new email
    /// </summary>
    /// <param name="email">The email address</param>
    public Email(string email)
	{
		email = email.Trim();

        if (!IsValid(email))
        {
            throw new ApplicationException($"Invalid {nameof(Email)}: {email}");
        }

        Value = email;

	}
    /// <summary>
    /// Validates the email address
    /// </summary>
    /// <param name="emailaddress"></param>
    /// <returns></returns>
    private static bool IsValid(string emailaddress)
    {
        try
        {
            MailAddress m = new(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }
}
