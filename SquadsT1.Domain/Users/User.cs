using SquadsT1.Domain.Reservations;
using SquadsT1.Domain.Common;
using Ardalis.GuardClauses;
using SquadsT1.Domain.Sessions;
using SquadsT1.Domain.Tokens;
using SquadsT1.Domain.Subscriptions;

namespace SquadsT1.Domain.Users;

public class User : Entity
{
    private List<Reservation> _reservations = new();
    private List<Token> _tokens = new();

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public Email Email { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public Address Address { get; set; }
    public Subscription? Subscription { get; private set; }
    public string PhysicalIssues { get; set; }
    public string DrugsUsed { get; set; }
    public int Length { get; set; }
    public bool OptedInOnNewsletter { get; set; }
    public bool OptedInOnWhatsapp { get; set; }
    public bool HasSignedHouseRules { get; set; }
    public bool IsTrainer { get; set; }

    /// <summary>
    /// Returns the reservations list as an immutable read only list
    /// </summary>
    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();
    /// <summary>
    /// Returns only the reservations that lie in the future list as an immutable read only list
    /// </summary>
    public IReadOnlyList<Reservation> PlannedReservations => _reservations.Where(r => r.Session.StartDate < DateTime.UtcNow).ToList().AsReadOnly();
    /// <summary>
    /// Returns only the reservations that lie in the past list as an immutable read only list
    /// </summary>
    public IReadOnlyList<Reservation> PassedReservations => _reservations.Where(r => r.Session.EndDate > DateTime.UtcNow).ToList().AsReadOnly();
    /// <summary>
    /// Returns the amount of planned reservations for this user
    /// </summary>
    public int AmountOfPlannedReservations => _reservations.Count(r => r.Session.StartDate > DateTime.UtcNow);
    /// <summary>
    /// Returns the amount of reservations in the past for this user
    /// </summary>
    public int AmoutOfPassedReservations => _reservations.Count(_r => _r.Session.EndDate < DateTime.UtcNow);
    /// <summary>
    /// Returns the amount of all reservations for this user
    /// </summary>
    public int AmountOfTotalReservations => _reservations.Count;
    /// <summary>
    /// Returns the tokens as an immutable read only list
    /// </summary>
    public IReadOnlyList<Token> Tokens => _tokens.AsReadOnly();
    /// <summary>
    /// Returns the amount of available tokens
    /// </summary>
    public int AmountOfAvailableTokens => _tokens.Count(t => !t.IsUsed);
    /// <summary>
    /// Returns the first available token or null when there are no more available tokens
    /// </summary>
    public Token? FirstAvailableToken => _tokens.FirstOrDefault(t => !t.IsUsed);
    /// <summary>
    /// Returns wether or not the user is able to book a reservation
    /// </summary>
    public bool HasActiveSubscription => DateTime.UtcNow >= Subscription?.GetLatestSubscriptionLine.ValidFrom && DateTime.UtcNow <= Subscription?.GetLatestSubscriptionLine.ValidTill;

    private User() { }

    public User(string firstName, string lastName, DateTime birthDate, Email email, PhoneNumber phoneNumber, Address address, string physicalIssues, string drugsUsed, int length, bool optedInOnNewsletter, bool optedInOnWhatsapp, bool hasSignedHouseRules, bool isTrainer)
    {
        FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
        LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
        BirthDate = birthDate;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        Subscription = null;
        PhysicalIssues = Guard.Against.NullOrWhiteSpace(physicalIssues, nameof(physicalIssues));
        DrugsUsed = Guard.Against.NullOrWhiteSpace(drugsUsed, nameof(drugsUsed));
        Length = Guard.Against.NegativeOrZero(length, nameof(length));
        OptedInOnNewsletter = optedInOnNewsletter;
        OptedInOnWhatsapp = optedInOnWhatsapp;
        HasSignedHouseRules = hasSignedHouseRules;
        IsTrainer = isTrainer;
        _tokens.Add(new Token(new Payment()));
    }

    /// <summary>
    /// Creates a new reservation for a given session and adds it to the reservations of the user
    /// </summary>
    /// <param name="session">The session for which a new reservation is created</param>
    public void ReserveSession(Session session)
    {
        if (AmountOfPlannedReservations < 3)
        {
            if (HasActiveSubscription)
            {
                _reservations.Add(new Reservation(this, session));
            }
            else if (AmountOfAvailableTokens > 0)
            {
                _reservations.Add(new Reservation(this, session));
                FirstAvailableToken?.UseToken();
            }
        }
    }

    /// <summary>
    /// Checks wether or not the user already has a subscription and adds a new subscription line
    /// </summary>
    /// <param name="validFrom"></param>
    /// <param name="validTill"></param>
    /// <param name="isPaid"></param>
    public void AddSubscription(DateTime validFrom, DateTime validTill, bool isPaid)
    {
        if (Subscription is null)
        {
            Subscription = new Subscription();
        }

        Subscription.RenewSubscription(validFrom, validTill, isPaid ? new Payment() : null);
    }
}