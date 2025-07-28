using AutoMapper;
using Schedule.Contracts.Dtos;
using Schedule.Contracts.Dtos.StaffRelated.Staff.Requests;
using Schedule.Contracts.Dtos.StaffRelated.Staff.Responses;
using Schedule.Domain.Models;
using Schedule.Domain.Models.Staff;

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
		CreateMap<Staff, StaffResponse>();
		CreateMap<CreateStaffRequest, Staff>();
		CreateMap<UpdateStaffRequest, Staff>();
	}
}