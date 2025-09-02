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

		CreateMap<CompanyRequest, Company>();
		CreateMap<Company, CompanyResponse>();

		CreateMap<StaffMember, StaffMemberResponse>()
			.ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
		CreateMap<StaffMemberRequest, StaffMember>();

		CreateMap<StaffMemberSpecializationRequest, StaffMemberSpecialization>()
			.ConstructUsing(src => new StaffMemberSpecialization(
				Guid.Empty,
				Guid.Empty,
				src.StaffMemberId,
				src.SpecializationId));

		CreateMap<StaffMemberAvailabilityRequest, StaffMemberAvailability>()
			.ConstructUsing(src => new StaffMemberAvailability(
				Guid.Empty,
				Guid.Empty,
				Guid.Empty,
				src.Date,
				src.StartTime,
				src.EndTime,
				true));
		CreateMap<StaffMemberAvailability, AvailabilityResponse>();
		CreateMap<(StaffMember staffMember, List<StaffMemberAvailability> availabilities),
				StaffMemberAvailabilityResponse>()
			.ConvertUsing((src, dest, context) =>
				new StaffMemberAvailabilityResponse(
					context.Mapper.Map<StaffMemberResponse>(src.staffMember),
					context.Mapper.Map<List<AvailabilityResponse>>(src.availabilities)));

		CreateMap<SpecializationRequest, Specialization>();
		CreateMap<Specialization, SpecializationResponse>();

		CreateMap<EventScheduleStaffMember, EventScheduleStaffMemberResponse>();
		CreateMap<EventScheduleStaffMemberRequest, EventScheduleStaffMember>()
			.ConstructUsing(src => new EventScheduleStaffMember(
				Guid.Empty,
				Guid.Empty,
				src.EventScheduleId,
				src.StaffMemberId));
		CreateMap<EventSchedule, EventScheduleResponse>();
	}
}