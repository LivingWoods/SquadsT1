﻿using SquadsT1.Domain.Reservations;
using SquadsT1.Domain.Common;
using SquadsT1.Domain.Users;
using Ardalis.GuardClauses;

namespace SquadsT1.Domain.Sessions;

public class Session : Entity
{
    private List<User> _waitlist = new();
    private List<Reservation> _reservations = new();

    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public SessionType SessionType { get; private set; }
    public User Trainer { get; private set; }
    public int? TrainerId { get; private set; }

    /// <summary>
    /// Returns the waitlist as an immutable readonly list
    /// </summary>
    public IReadOnlyList<User> Waitlist => _waitlist.AsReadOnly();
    /// <summary>
    /// Returns the amount of user already registered on the waitlist
    /// </summary>
    public int AmountOfUsersOnWaitlist => _waitlist.Count;
    /// <summary>
    /// Returns wether or not a user still can reserve a spot for this session
    /// </summary>
    public bool CanBeReserved => DateTime.UtcNow < StartDate.AddHours(-1) && _reservations.Count < 6;
    /// <summary>
    /// Returns wether or not a user still can cancel his reservation for this session
    /// </summary>
    public bool ReservationCanBeCanceled => DateTime.UtcNow < StartDate.AddHours(-2);
    /// <summary>
    /// Returns the reservations list as an immutable read only list
    /// </summary>
    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();
    /// <summary>
    /// Returns the amount of all reservations for this session
    /// </summary>
    public int AmountOfTotalReservations => _reservations.Count;

    private Session() { }
    /// <summary>
    /// Validates and creates a new session
    /// </summary>
    /// <param name="startDate">The start date of the session</param>
    /// <param name="endDate">The end date of the session</param>
    /// <param name="sessionType">the type of the session</param>
    /// <param name="trainer">The trainer that will be giving the session</param>
    public Session(DateTime startDate, DateTime endDate, SessionType sessionType, User trainer)
    {
        StartDate = Guard.Against.OutOfRange(startDate, nameof(startDate), DateTime.UtcNow, DateTime.MaxValue, "Session week start date can not be in the past");
        EndDate = Guard.Against.OutOfRange(endDate, nameof(endDate), StartDate, DateTime.MaxValue, "Session week end date should be greater than the start date of the session");
        SessionType = Guard.Against.EnumOutOfRange(sessionType, nameof(sessionType), "Session type should exist");
        Trainer = Guard.Against.Null(trainer, nameof(trainer), "Trainer can not be null");
    }

    /// <summary>
    /// Registers a new user on the waitlist
    /// </summary>
    /// <param name="user">The user to be added to the waitlist</param>
    public void AddUserToWaitlist(User user)
    {
        if (!_waitlist.Contains(user))
        {
            _waitlist.Add(user);
        }
    }

    /// <summary>
    /// Removes a registered user from the waitlist
    /// </summary>
    /// <param name="user">The user that should be removed from the waitlist</param>
    public void RemoveUserFromWaitlist(User user)
    {
        if (_waitlist.Contains(user))
        {
            _waitlist.Remove(user);
        }
    }

    /// <summary>
    /// Changes the session type of the session to the specified session type
    /// </summary>
    /// <param name="sessionType">The new session type</param>
    public void ChangeSessionType(SessionType sessionType)
    {
        SessionType = sessionType;
    }
}
