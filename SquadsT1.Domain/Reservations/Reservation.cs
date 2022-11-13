using SquadsT1.Domain.Sessions;
using SquadsT1.Domain.Common;
using SquadsT1.Domain.Users;
using Ardalis.GuardClauses;

namespace SquadsT1.Domain.Reservations;

public class Reservation : Entity
{
    public int UserId { get; }
    public User User { get; }
    public int SessionId { get; }
    public Session Session { get; }

    private Reservation() { }
    public Reservation(User user, Session session)
    {
        User = Guard.Against.Null(user, nameof(user));
        Session = Guard.Against.Null(session, nameof(session));
        Guard.Against.OutOfRange(Session.AmountOfTotalReservations + 1, nameof(Session.Reservations), 0, 6);
    }
}