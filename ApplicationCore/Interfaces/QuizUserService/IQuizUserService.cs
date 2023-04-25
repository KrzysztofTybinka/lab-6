using ApplicationCore.Interfaces.Repository;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces;

public interface IQuizUserService
{
    Quiz CreateAndGetQuizRandom(int count);

    IEnumerable<Quiz> FindAllQuizzes();

    Quiz? FindQuizById(int id);

    QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int quizItemId, int userId, string answer);

    List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId);

    int CountCorrectAnswersForQuizFilledByUser(int quizId, int userId)
    {
        return GetUserAnswersForQuiz(quizId, userId)
            .Count(e => e.IsCorrect());
    }
}