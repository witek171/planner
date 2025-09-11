using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class ReservationResponse
{
	[Required] public Guid Id { get; private set; }
	[Required] public EventScheduleResponse EventSchedule { get; private set; }
	[Required] public IReadOnlyList<ParticipantResponse> Participants { get; private set; }
	[Required] public string Status { get; private set; }
	[Required] public string Notes { get; private set; }
	[Required] public DateTime CreatedAt { get; private set; }
	public DateTime? CancelledAt { get; private set; }
	public DateTime? PaidAt { get; private set; }
}