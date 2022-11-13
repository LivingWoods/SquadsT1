using SquadsT1.Domain.Reservations;
using SquadsT1.Domain.Common;
using Ardalis.GuardClauses;

namespace SquadsT1.Domain.Users;

public class User : Entity
{
    private List<Reservation> _reservations = new();

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; }
    public string PhysicalIssues { get; set; }
    public string DrugsUsed { get; set; }
    public int Length { get; set; }
    public bool OptedInOnNewsletter { get; set; }
    public bool OptedInOnWhatsapp { get; set; }
    public bool HasSignedHouseRules { get; set; }
    public bool IsTrainer { get; set; }

    private User() { }

    public User(string firstName, string lastName, DateTime birthDate, string phoneNumber, string physicalIssues, string drugsUsed, int length, bool optedInOnNewsletter, bool optedInOnWhatsapp, bool hasSignedHouseRules, bool isTrainer)
    {
        FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
        LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
        PhysicalIssues = physicalIssues;
        DrugsUsed = drugsUsed;
        Length = length;
        OptedInOnNewsletter = optedInOnNewsletter;
        OptedInOnWhatsapp = optedInOnWhatsapp;
        HasSignedHouseRules = hasSignedHouseRules;
        IsTrainer = isTrainer;
    }

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
}