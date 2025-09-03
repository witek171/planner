using AutoMapper;
using Schedule.Contracts.Dtos.Requests;
using Schedule.Contracts.Dtos.Responses;
using Schedule.Domain.Models;

namespace Schedule.API.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<ApplicationHealthStatus, ApplicationHealthStatusResponse>();
		CreateMap<DatabaseHealthStatus, DatabaseHealthStatusResponse>();

		CreateMap<ParticipantCreateRequest, Participant>();
		CreateMap<Participant, ParticipantResponse>();
		CreateMap<ParticipantUpdateRequest, Participant>();

		CreateMap<CompanyRequest, Company>();
		CreateMap<Company, CompanyResponse>();

		CreateMap<StaffMember, StaffMemberResponse>()
			.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
		CreateMap<StaffMemberRequest, StaffMember>();

		CreateMap<StaffMemberSpecializationRequest, StaffMemberSpecialization>();

		CreateMap<StaffMemberAvailabilityRequest, StaffMemberAvailability>();
		CreateMap<StaffMemberAvailability, AvailabilityResponse>();
		CreateMap<(StaffMember staffMember, List<StaffMemberAvailability> availabilities),
			StaffMemberAvailabilityResponse>();

		CreateMap<SpecializationRequest, Specialization>();
		CreateMap<Specialization, SpecializationResponse>();

		CreateMap<EventScheduleStaffMember, EventScheduleStaffMemberResponse>();
		CreateMap<EventScheduleStaffMemberRequest, EventScheduleStaffMember>();

		CreateMap<EventSchedule, EventScheduleResponse>();
	}
}