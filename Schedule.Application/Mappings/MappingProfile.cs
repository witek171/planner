using AutoMapper;
using Schedule.Contracts.Dtos.Staff.Responses;
using Schedule.Contracts.Dtos.Staff.Requests;
using Schedule.Domain.Models;

namespace Schedule.Application.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<CreateStaffRequest, Staff>();

		CreateMap<UpdateStaffRequest, Staff>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

		CreateMap<Staff, StaffResponse>();
	}
}
