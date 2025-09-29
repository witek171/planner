namespace Schedule.Domain.Exceptions;

public class EmailAlreadyExistsException : Exception
{
	public EmailAlreadyExistsException()
		: base("Email already exists for another staff member in this company.") { }

	public EmailAlreadyExistsException(string message)
		: base(message) { }

	public EmailAlreadyExistsException(string message, Exception innerException)
		: base(message, innerException) { }
}
