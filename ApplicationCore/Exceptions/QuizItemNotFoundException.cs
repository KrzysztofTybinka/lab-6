namespace ApplicationCore.Exceptions;

public class QuizItemNotFoundException: Exception
{
    public QuizItemNotFoundException(string message): base(message)
    {
    }
}