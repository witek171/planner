namespace Schedule.Domain.Exceptions;

public class InvalidCredentialsException : Exception
{
	public InvalidCredentialsException() : base("Incorect password or email") { }
}
