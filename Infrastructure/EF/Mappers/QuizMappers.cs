using ApplicationCore.Models;
using Infrastructure.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Mappers
{
    public static class QuizMappers
    {
        public static QuizItem FromEntityToQuizItem(QuizItemEntity entity)
        {
            return new QuizItem(
                entity.Id,
                entity.Question,
                entity.IncorrectAnswers.Select(e => e.Answer).ToList(),
                entity.CorrectAnswer);
        }

        public static Quiz FromEntityToQuiz(QuizEntity entity)
        {
            var quizItems = entity.Items.Select(x => new QuizItem(x.Id, x.Question, x.IncorrectAnswers.Select(y => y.Answer).ToList(), x.CorrectAnswer)).ToList();
            return new Quiz(entity.Id, quizItems, entity.Title);
        }
    }
}