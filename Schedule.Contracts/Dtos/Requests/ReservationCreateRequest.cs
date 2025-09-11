using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class ReservationCreateRequest
{
	[Required] public Guid EventScheduleId { get; }
	[Required] public string Notes { get; }
	[Required] public IReadOnlyList<Guid> ParticipantsIds { get; }

	public ReservationCreateRequest(
		Guid eventScheduleId,
		string notes, IReadOnlyList<Guid>
			participantsIds)
	{
		EventScheduleId = eventScheduleId;
		Notes = notes;
		ParticipantsIds = participantsIds;
	}
}