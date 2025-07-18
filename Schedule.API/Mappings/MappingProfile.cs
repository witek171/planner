using AutoMapper;
using Schedule.Domain.Dtos;
using Schedule.Domain.Models;

namespace PlannerNet.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<ApplicationHealthStatus, ApplicationHealthStatusDto>()
			.ConstructUsing(src => new ApplicationHealthStatusDto(
					src.Version,
					src.Environment,
					src.Uptime,
					src.MemoryUsage,
					src.Status,
					src.Timestamp,
					src.Details ?? new Dictionary<string, object>()
				)
			);

		CreateMap<DatabaseHealthStatus, DatabaseHealthStatusDto>()
			.ConstructUsing(src => new DatabaseHealthStatusDto(
					src.ConnectionString,
					src.ResponseTime,
					src.DatabaseName,
					src.Status,
					src.Timestamp,
					src.Details ?? new Dictionary<string, object>()
				)
			);
	}
}