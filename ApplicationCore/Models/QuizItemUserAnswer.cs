using ApplicationCore.Interfaces.Repository;

namespace ApplicationCore.Models;

public class QuizItemUserAnswer: IIdentity<string>
{
    public int QuizId { get; init; }
    public QuizItem  QuizItem{ get; init; }
    public int UserId { get; init; }
    public string Answer { get; init; }
    public QuizItemUserAnswer(QuizItem quizItem, int userId, int quizId,string answer)
    {
        QuizItem = quizItem;
        Answer = answer;
        UserId = userId;
        QuizId = quizId;
    }
    public QuizItemUserAnswer()
    {
    }
    public bool IsCorrect()
    {
        return QuizItem.CorrectAnswer == Answer;
    }
    public string Id
    {
        get => $"{QuizId}{UserId}{QuizItem.Id}";
        set
        {
            
        }
    }
}