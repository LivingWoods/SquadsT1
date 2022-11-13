using Ardalis.GuardClauses;
using SquadsT1.Domain.Common;

namespace SquadsT1.Domain.Sessions;

public class SessionWeek : Entity
{
    private List<SessionDay> _sessionDays = new();

    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    /// <summary>
    /// Returns the sessions days list as an immutable readonly list
    /// </summary>
    public IReadOnlyList<SessionDay> SessionDays => _sessionDays.AsReadOnly();

    private SessionWeek() { }
    /// <summary>
    /// Validates and creates a new session week
    /// </summary>
    /// <param name="startDate">The start date of the session day</param>
    /// <param name="endDate">The end date of the session day</param>
    public SessionWeek(DateTime startDate, DateTime endDate)
    {
        StartDate = Guard.Against.OutOfRange(startDate, nameof(startDate), DateTime.UtcNow, DateTime.MaxValue, "Session week start date can not be in the past");
        EndDate = Guard.Against.OutOfRange(endDate, nameof(endDate), StartDate, new DateTime(startDate.Year, startDate.Month, startDate.AddDays(6).Day), "Session week end date should be on the same day as the start date");
    }

    public void AddSessionDayToSessionDays(DateTime startDate, DateTime endDate)
    {
        _sessionDays.Add(new SessionDay(startDate, endDate));
    }
}
