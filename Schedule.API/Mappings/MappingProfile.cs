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

		CreateMap<StaffMember, StaffMemberResponse>()
			.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
		CreateMap<CreateStaffMemberRequest, StaffMember>();
		CreateMap<UpdateStaffMemberRequest, StaffMember>();

		CreateMap<CreateStaffMemberSpecializationRequest, StaffMemberSpecialization>()
			.ConstructUsing(src => new StaffMemberSpecialization(
				Guid.Empty,
				Guid.Empty,
				src.StaffMemberId,
				src.SpecializationId));

		CreateMap<CreateStaffMemberAvailabilityRequest, StaffMemberAvailability>()
			.ConstructUsing(src => new StaffMemberAvailability(
				Guid.Empty,
				Guid.Empty,
				Guid.Empty,
				src.Date,
				src.StartTime,
				src.EndTime,
				true));
		CreateMap<StaffMemberAvailability, StaffMemberAvailabilityResponse>();

		CreateMap<Specialization, SpecializationResponse>();
		
		CreateMap<EventScheduleStaffMember, EventScheduleStaffMemberResponse>();
		CreateMap<CreateEventScheduleStaffMemberRequest, EventScheduleStaffMember>()
			.ConstructUsing(src => new EventScheduleStaffMember(
				Guid.Empty,
				Guid.Empty,
				Guid.Empty,
				Guid.Empty));
	}
}