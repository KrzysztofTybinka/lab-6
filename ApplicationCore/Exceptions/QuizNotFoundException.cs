namespace ApplicationCore.Exceptions;

public class QuizNotFoundException: Exception
{
    public QuizNotFoundException(string? message) : base(message)
    {
    }
}