using AutoMapper;
using Schedule.Contracts.Dtos.Requests;
using Schedule.Contracts.Dtos.Responses;
using Schedule.Domain.Models;

namespace PlannerNet.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<ApplicationHealthStatus, ApplicationHealthStatusResponse>();

		CreateMap<DatabaseHealthStatus, DatabaseHealthStatusResponse>()
			.ConstructUsing(src => new DatabaseHealthStatusResponse(
					src.ConnectionString,
					src.ResponseTime,
					src.DatabaseName,
					src.Status,
					src.Timestamp,
					src.Details
				)
			);

		CreateMap<ParticipantCreateRequest, Participant>();
		CreateMap<Participant, ParticipantResponse>();
		CreateMap<ParticipantUpdateRequest, Participant>();

		CreateMap<StaffMember, StaffMemberResponse>();
		CreateMap<CreateStaffMemberRequest, StaffMember>();
		CreateMap<UpdateStaffMemberRequest, StaffMember>();

		CreateMap<CreateStaffMemberSpecializationRequest, StaffMemberSpecialization>();
		CreateMap<StaffMemberSpecialization, StaffMemberSpecializationResponse>();

		CreateMap<CreateStaffMemberAvailabilityRequest, StaffMemberAvailability>();
		CreateMap<UpdateStaffMemberAvailabilityRequest, StaffMemberAvailability>();
		CreateMap<StaffMemberAvailability, StaffMemberAvailabilityResponse>();
	}
}