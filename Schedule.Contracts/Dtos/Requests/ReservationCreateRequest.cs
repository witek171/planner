using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class ReservationCreateRequest
{
	[Required] public Guid EventScheduleId { get; }
	[Required] public string Notes { get; }
	[Required] public IReadOnlyList<Guid> ParticipantsIds { get; }
	[Required] public bool IsPaid { get; }

	public ReservationCreateRequest(
		Guid eventScheduleId,
		string notes,
		IReadOnlyList<Guid> participantsIds,
		bool isPaid)
	{
		EventScheduleId = eventScheduleId;
		Notes = notes;
		ParticipantsIds = participantsIds;
		IsPaid = isPaid;
	}
}