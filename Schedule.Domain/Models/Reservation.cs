using Schedule.Domain.Models.Enums;

namespace Schedule.Domain.Models;

public class Reservation
{
	public Guid Id { get; }
	public Guid CompanyId { get; private set; }
	public Guid EventScheduleId { get; private set; }
	public EventSchedule EventSchedule { get; private set; }
	public IReadOnlyList<Guid> ParticipantsIds { get; private set; }
	public IReadOnlyList<Participant> Participants { get; private set; }
	public ReservationStatus Status { get; private set; }
	public string Notes { get; private set; }
	public DateTime CreatedAt { get; }
	public DateTime? CancelledAt { get; private set; }
	public DateTime? PaidAt { get; private set; }

	public Reservation(
		Guid id,
		Guid companyId,
		Guid eventScheduleId,
		EventSchedule eventSchedule,
		IReadOnlyList<Guid> participantsIds,
		IReadOnlyList<Participant> participants,
		ReservationStatus status,
		string notes,
		DateTime createdAt,
		DateTime? cancelledAt = null,
		DateTime? paidAt = null)
	{
		Id = id;
		CompanyId = companyId;
		EventScheduleId = eventScheduleId;
		EventSchedule = eventSchedule;
		ParticipantsIds = participantsIds;
		Participants = participants;
		Status = status;
		Notes = notes;
		CreatedAt = createdAt;
		CancelledAt = cancelledAt;
		PaidAt = paidAt;
	}

	public Reservation()
	{
	}

	public void SetCompanyId(Guid companyId)
	{
		if (CompanyId != Guid.Empty)
			throw new InvalidOperationException(
				$"CompanyId is already set to {CompanyId} and cannot be changed");

		CompanyId = companyId;
	}

	public void SetParticipants(List<Participant> participants)
	{
		if (Participants.Any())
			throw new InvalidOperationException(
				$"Participants for reservation {Id} are already set and cannot be changed");

		Participants = participants;
	}

	public void Normalize()
		=> Notes = Notes.Trim();

	public void SoftDelete()
	{
		if (Status == ReservationStatus.Cancelled)
			throw new InvalidOperationException(
				$"Reservation {Id} is already marked as cancelled");

		Status = ReservationStatus.Cancelled;
		CancelledAt = DateTime.UtcNow;
	}
}