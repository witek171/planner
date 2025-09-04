using Schedule.Domain.Models.Enums;

namespace Schedule.Domain.Models;

public class Reservation
{
	public Guid Id { get; }
	public Guid CompanyId { get; private set; }
	public Guid ParticipantId { get; private set; }
	public Guid EventScheduleId { get; private set; }
	public int ParticipantCount { get; }
	public ReservationStatus Status { get; private set; }
	public string Notes { get; private set; }
	public DateTime CreatedAt { get; }
	public DateTime CancelledAt { get; private set; }
	public DateTime PaidAt { get; }

	public Reservation(
		Guid id,
		Guid companyId,
		Guid participantId,
		Guid eventScheduleId,
		int participantCount,
		ReservationStatus status,
		string notes,
		DateTime createdAt,
		DateTime cancelledAt,
		DateTime paidAt)
	{
		Id = id;
		CompanyId = companyId;
		ParticipantId = participantId;
		EventScheduleId = eventScheduleId;
		ParticipantCount = participantCount;
		Status = status;
		Notes = notes;
		CreatedAt = createdAt;
		CancelledAt = cancelledAt;
		PaidAt = paidAt;
	}

	public void SetCompanyId(Guid companyId)
	{
		if (CompanyId != Guid.Empty)
			throw new InvalidOperationException(
				$"CompanyId is already set to {CompanyId} and cannot be changed");

		CompanyId = companyId;
	}

	public void SetParticipantId(Guid participantId)
	{
		if (ParticipantId != Guid.Empty)
			throw new InvalidOperationException(
				$"ParticipantId is already set to {ParticipantId} and cannot be changed");

		ParticipantId = participantId;
	}

	public void SetEventScheduleId(Guid eventScheduleId)
	{
		if (EventScheduleId != Guid.Empty)
			throw new InvalidOperationException(
				$"EventScheduleId is already set to {EventScheduleId} and cannot be changed");

		EventScheduleId = eventScheduleId;
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