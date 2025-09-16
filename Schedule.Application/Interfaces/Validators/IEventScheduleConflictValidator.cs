namespace Schedule.Application.Interfaces.Validators;

public interface IEventScheduleConflictValidator
{
	Task<bool> CanAssignStaffMemberAsync(
		Guid companyId,
		Guid staffMemberId,
		DateTime start,
		DateTime end);

	Task<bool> CanAssignParticipantAsync(
		Guid companyId,
		Guid participantId,
		DateTime start,
		DateTime end);
}