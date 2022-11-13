using SquadsT1.Domain.Common;
using Ardalis.GuardClauses;
using SquadsT1.Domain.Users;

namespace SquadsT1.Domain.Sessions;

public class SessionDay : Entity
{
    private List<Session> _sessions = new();

    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    /// <summary>
    /// Returns the day of the week
    /// </summary>
    public DayOfWeek DayOfWeek => StartDate.DayOfWeek;
    /// <summary>
    /// Returns the sessions list as an immutable readonly list
    /// </summary>
    public IReadOnlyList<Session> Sessions => _sessions.AsReadOnly();

    /// <summary>
    /// Validates and creates a new session day
    /// </summary>
    /// <param name="startDate">The start date of the session day</param>
    /// <param name="endDate">The end date of the session day</param>
    public SessionDay(DateTime startDate, DateTime endDate)
    {
        StartDate = Guard.Against.OutOfRange(startDate, nameof(startDate), DateTime.UtcNow, DateTime.MaxValue, "Session day start date can not be in the past");
        EndDate = Guard.Against.OutOfRange(endDate, nameof(endDate), StartDate, new DateTime(startDate.Year, startDate.Month, startDate.AddDays(1).Day), "Session day end date should be on the same day as the start date");
    }

    /// <summary>
    /// Adds a new session to the session list of that day
    /// </summary>
    /// <param name="startDate">The start date of the session</param>
    /// <param name="endDate">The end date of the session</param>
    /// <param name="sessionType">The type of session</param>
    /// <param name="trainer">The trainer that will be giving that session</param>
    public void AddSessionToSessions(DateTime startDate, DateTime endDate, SessionType sessionType, User trainer)
    {
        _sessions.Add(new Session(startDate, endDate, sessionType, trainer));
    }

    /// <summary>
    /// Removes the specified session from the session list of that day
    /// </summary>
    /// <param name="sessionId"></param>
    public void RemoveSessionFromSessions(int sessionId)
    {
        Session? sessionToRemove = _sessions.FirstOrDefault(s => s.Id == sessionId);

        if (sessionToRemove is not null)
        {
            _sessions.Remove(sessionToRemove);
        }
    }
}
