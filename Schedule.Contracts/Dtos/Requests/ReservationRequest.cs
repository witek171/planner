using System.ComponentModel.DataAnnotations;

namespace Schedule.Contracts.Dtos.Requests;

public class ReservationRequest
{
	[Required] public Guid EventScheduleId { get; }
	[Required] public string Notes { get; }
	[Required] public IReadOnlyList<Guid> ParticipantsIds { get; }

	public ReservationRequest(
		Guid eventScheduleId,
		string notes, IReadOnlyList<Guid>
			participantsIds)
	{
		EventScheduleId = eventScheduleId;
		Notes = notes;
		ParticipantsIds = participantsIds;
	}
}