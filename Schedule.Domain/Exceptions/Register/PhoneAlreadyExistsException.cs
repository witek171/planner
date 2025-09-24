namespace Schedule.Domain.Exceptions.Register;

public class PhoneAlreadyExistsException : Exception
{
	public PhoneAlreadyExistsException()
	: base("Phone already exists for another staff member in this company.") { }

	public PhoneAlreadyExistsException(string message)
		: base(message) { }

	public PhoneAlreadyExistsException(string message, Exception innerException)
		: base(message, innerException) { }
}
