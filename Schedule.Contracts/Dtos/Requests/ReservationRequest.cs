using System.ComponentModel.DataAnnotations;
using Schedule.Domain.Models.Enums;

namespace Schedule.Contracts.Dtos.Requests;

public class ReservationRequest
{
	[Required] public int ParticipantCount { get; }
	[Required] public ReservationStatus Status { get; }
	[Required] public string Notes { get; }
	[Required] public DateTime CreatedAt { get; }
	[Required] public DateTime CancelledAt { get; }
	[Required] public DateTime PaidAt { get; }

	public ReservationRequest(
		int participantCount,
		ReservationStatus status,
		string notes,
		DateTime createdAt,
		DateTime cancelledAt,
		DateTime paidAt)
	{
		ParticipantCount = participantCount;
		Status = status;
		Notes = notes;
		CreatedAt = createdAt;
		CancelledAt = cancelledAt;
		PaidAt = paidAt;
	}
}