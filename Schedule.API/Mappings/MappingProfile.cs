using AutoMapper;
using Schedule.Contracts.Dtos;
using Schedule.Domain.Models;

namespace PlannerNet.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<ApplicationHealthStatus, ApplicationHealthStatusResponse>()
			.ConstructUsing(src => new ApplicationHealthStatusResponse(
					src.Version,
					src.Environment,
					src.Uptime,
					src.MemoryUsage,
					src.Status,
					src.Timestamp,
					src.Details
				)
			);

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
		
		CreateMap<ParticipantCreateRequest, Participant>()
			.ConstructUsing(src => new Participant(
					Guid.NewGuid(),
					src.CompanyId,
					src.Email,
					src.FirstName,
					src.LastName,
					src.Phone,
					src.GdprConsent,
					DateTime.UtcNow
				)
			);
	}
}