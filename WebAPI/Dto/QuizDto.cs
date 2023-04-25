using ApplicationCore.Models;

namespace WebAPI.Controllers;

public class QuizDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<QuizItemDto> Items { get; set; }
    
    public static QuizDto of(Quiz quiz)
    {
        if (quiz is null)
        {
            return null;
        }
        return new QuizDto()
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Items = quiz.Items.Select(QuizItemDto.of).ToList()
        };
    }
}