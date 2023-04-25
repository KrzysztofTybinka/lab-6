namespace WebAPI.Controllers;

public class FeedbackQuizDto
{ 
   public int QuizId { get; init; }
   
   public int UserId { get; set; }
   
   public List<FeedbackQuizItemDto> QuizItemsAnswers{get; init; }
}