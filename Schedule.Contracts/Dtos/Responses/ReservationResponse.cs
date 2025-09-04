using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Responses;

public class ReservationResponse
{
	[Required] public Guid Id { get; }
	[Required] public Guid ParticipantId { get; }
	[Required] public Guid EventScheduleId { get; }
	[Required] public int ParticipantCount { get; }
	[Required] public string Status { get; }
	[Required] public string Notes { get; }
	[Required] public DateTime CreatedAt { get; }
	[Required] public DateTime CancelledAt { get; }
	[Required] public DateTime PaidAt { get; }

	public ReservationResponse(
		Guid id,
		Guid participantId,
		Guid eventScheduleId,
		int participantCount,
		string status,
		string notes,
		DateTime createdAt,
		DateTime cancelledAt,
		DateTime paidAt)
	{
		Id = id;
		ParticipantId = participantId;
		EventScheduleId = eventScheduleId;
		ParticipantCount = participantCount;
		Status = status;
		Notes = notes;
		CreatedAt = createdAt;
		CancelledAt = cancelledAt;
		PaidAt = paidAt;
	}
}