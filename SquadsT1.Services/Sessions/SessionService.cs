using ProtoBuf.Grpc;
using SquadsT1.Shared.Sessions;

namespace SquadsT1.Services.Sessions;

public class SessionService : ISessionService
{
    private static List<SessionDto.Index> _sessions = new List<SessionDto.Index>();

	public SessionService()
	{
        var SessionMon3 = new SessionDto.Index();
        SessionMon3.Id = 17;
        SessionMon3.StartDate = new DateTime(2022, 11, 14, 19, 0, 0);
        SessionMon3.EndDate = new DateTime(2022, 11, 14, 20, 0, 0);
        SessionMon3.SpotsLeft = 1;
        SessionMon3.SessionType = "Heavy Workout";
        SessionMon3.Trainer = "Hells.";
        _sessions.Add(SessionMon3);

        var SessionTue3 = new SessionDto.Index();
        SessionTue3.Id = 18;
        SessionTue3.StartDate = new DateTime(2022, 11, 15, 19, 0, 0);
        SessionTue3.EndDate = new DateTime(2022, 11, 15, 20, 0, 0);
        SessionTue3.SpotsLeft = 2;
        SessionTue3.SessionType = "Heavy Workout";
        SessionTue3.Trainer = "Beast Mode.";
        _sessions.Add(SessionTue3);

        var SessionTueTue3 = new SessionDto.Index();
        SessionTueTue3.Id = 19;
        SessionTueTue3.StartDate = new DateTime(2022, 11, 15, 20, 0, 0);
        SessionTueTue3.EndDate = new DateTime(2022, 11, 15, 21, 0, 0);
        SessionTueTue3.SpotsLeft = 3;
        SessionTueTue3.SessionType = "Heavy Workout";
        SessionTueTue3.Trainer = "Beast Mode.";
        _sessions.Add(SessionTueTue3);

        var SessionWhe3 = new SessionDto.Index();
        SessionWhe3.Id = 18;
        SessionWhe3.StartDate = new DateTime(2022, 11, 16, 19, 0, 0);
        SessionWhe3.EndDate = new DateTime(2022, 11, 16, 20, 0, 0);
        SessionWhe3.SpotsLeft = 4;
        SessionWhe3.SessionType = "Heavy Workout";
        SessionWhe3.Trainer = "Hells.";
        _sessions.Add(SessionWhe3);

        var SessionThu3 = new SessionDto.Index();
        SessionThu3.Id = 19;
        SessionThu3.StartDate = new DateTime(2022, 11, 17, 19, 0, 0);
        SessionThu3.EndDate = new DateTime(2022, 11, 17, 20, 0, 0);
        SessionThu3.SpotsLeft = 0;
        SessionThu3.SessionType = "Heavy Workout";
        SessionThu3.Trainer = "Beast Mode.";
        _sessions.Add(SessionThu3);

        var SessionSat3 = new SessionDto.Index();
        SessionSat3.Id = 20;
        SessionSat3.StartDate = new DateTime(2022, 11, 19, 19, 0, 0);
        SessionSat3.EndDate = new DateTime(2022, 11, 19, 20, 0, 0);
        SessionSat3.SpotsLeft = 5;
        SessionSat3.SessionType = "Yoga";
        SessionSat3.Trainer = "Hells.";
        _sessions.Add(SessionSat3);

        var SessionSun3 = new SessionDto.Index();
        SessionSun3.Id = 21;
        SessionSun3.StartDate = new DateTime(2022, 11, 20, 19, 0, 0);
        SessionSun3.EndDate = new DateTime(2022, 11, 20, 20, 0, 0);
        SessionSun3.SpotsLeft = 2;
        SessionSun3.SessionType = "Heavy Workout";
        SessionSun3.Trainer = "Beast Mode.";
        _sessions.Add(SessionSun3);
    }

    public Task<IEnumerable<SessionReply.Index>> GetAllSessionsAsync(SessionRequest.Index request, CallContext context = default)
    {
        return Task.FromResult(_sessions.Select(s => new SessionReply.Index
        {
            Id = s.Id,
            StartDate = s.StartDate,
            EndDate = s.EndDate,
            SpotsLeft = s.SpotsLeft,
            SessionType = s.SessionType,
            Trainer = s.Trainer
        }));
    }
}
