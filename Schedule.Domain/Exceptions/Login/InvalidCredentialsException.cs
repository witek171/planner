namespace Schedule.Domain.Exceptions.Login;

public class InvalidCredentialsException : Exception
{
	public InvalidCredentialsException() : base("Incorect password or email") { }
}
