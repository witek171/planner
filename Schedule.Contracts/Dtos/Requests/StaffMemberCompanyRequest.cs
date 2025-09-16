namespace Schedule.Contracts.Dtos.Requests
{
	public class StaffMemberCompanyRequest
	{
		public Guid StaffMemberId { get; set; }
		public Guid CompanyId { get; set; }
	}
}