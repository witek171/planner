using Microsoft.Data.SqlClient;
using Schedule.Domain.Models;
using Schedule.Domain.Models.Enums;

namespace Schedule.Infrastructure.Utils;

public static class DbMapper
{
	public static Company MapCompany(SqlDataReader reader)
	{
		return new Company(
			reader.GetGuid(reader.GetOrdinal("Id")),
			reader.GetString(reader.GetOrdinal("Name")),
			reader.GetString(reader.GetOrdinal("TaxCode")),
			reader.GetString(reader.GetOrdinal("Street")),
			reader.GetString(reader.GetOrdinal("City")),
			reader.GetString(reader.GetOrdinal("PostalCode")),
			reader.GetString(reader.GetOrdinal("Phone")),
			reader.GetString(reader.GetOrdinal("Email")),
			reader.GetBoolean(reader.GetOrdinal("IsParentNode")),
			reader.GetBoolean(reader.GetOrdinal("IsReception")),
			reader.GetDateTime(reader.GetOrdinal("CreatedAt")));
	}

	public static EventSchedule MapEventSchedule(SqlDataReader reader)
	{
		return new EventSchedule(
			reader.GetGuid(reader.GetOrdinal("Id")),
			reader.GetGuid(reader.GetOrdinal("CompanyId")),
			reader.GetGuid(reader.GetOrdinal("EventTypeId")),
			reader.GetString(reader.GetOrdinal("PlaceName")),
			reader.GetDateTime(reader.GetOrdinal("StartTime")),
			reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
			reader.GetString(reader.GetOrdinal("Status")));
	}

	public static EventScheduleStaffMember MapEventScheduleStaffMember(SqlDataReader reader)
	{
		return new EventScheduleStaffMember(
			reader.GetGuid(reader.GetOrdinal("Id")),
			reader.GetGuid(reader.GetOrdinal("CompanyId")),
			reader.GetGuid(reader.GetOrdinal("EventScheduleId")),
			reader.GetGuid(reader.GetOrdinal("StaffMemberId")));
	}

	public static Participant MapParticipant(SqlDataReader reader)
	{
		return new Participant(
			reader.GetGuid(reader.GetOrdinal("Id")),
			reader.GetGuid(reader.GetOrdinal("CompanyId")),
			reader.GetString(reader.GetOrdinal("Email")),
			reader.GetString(reader.GetOrdinal("FirstName")),
			reader.GetString(reader.GetOrdinal("LastName")),
			reader.GetString(reader.GetOrdinal("Phone")),
			reader.GetBoolean(reader.GetOrdinal("GdprConsent")),
			reader.GetDateTime(reader.GetOrdinal("CreatedAt")));
	}

	public static Specialization MapSpecialization(SqlDataReader reader)
	{
		return new Specialization(
			reader.GetGuid(reader.GetOrdinal("Id")),
			reader.GetGuid(reader.GetOrdinal("CompanyId")),
			reader.GetString(reader.GetOrdinal("Name")),
			reader.GetString(reader.GetOrdinal("Description")));
	}

	public static StaffMemberAvailability MapStaffMemberAvailability(SqlDataReader reader)
	{
		return new StaffMemberAvailability(
			reader.GetGuid(reader.GetOrdinal("Id")),
			reader.GetGuid(reader.GetOrdinal("CompanyId")),
			reader.GetGuid(reader.GetOrdinal("StaffMemberId")),
			DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Date"))),
			reader.GetDateTime(reader.GetOrdinal("StartTime")),
			reader.GetDateTime(reader.GetOrdinal("EndTime")),
			reader.GetBoolean(reader.GetOrdinal("IsAvailable")));
	}

	public static StaffMember MapStaffMember(SqlDataReader reader)
	{
		return new StaffMember(
			reader.GetGuid(reader.GetOrdinal("StaffMemberId")),
			reader.GetGuid(reader.GetOrdinal("CompanyId")),
			Enum.Parse<StaffRole>(reader.GetString(reader.GetOrdinal("Role"))),
			reader.GetString(reader.GetOrdinal("Email")),
			reader.GetString(reader.GetOrdinal("Password")),
			reader.GetString(reader.GetOrdinal("FirstName")),
			reader.GetString(reader.GetOrdinal("LastName")),
			reader.GetString(reader.GetOrdinal("Phone")),
			reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
			reader.GetBoolean(reader.GetOrdinal("IsDeleted")),
			new List<Specialization>());
	}
}