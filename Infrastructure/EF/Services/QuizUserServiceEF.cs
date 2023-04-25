using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;

using ApplicationCore.Models;
using Infrastructure.EF.Entities;
using Infrastructure.EF.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.EF.Services
{
    public class QuizUserServiceEF : IQuizUserService
    {
        private readonly QuizDbContext _context;

        public QuizUserServiceEF(QuizDbContext context) //10
        {
            this._context = context;
        }

        public Quiz CreateAndGetQuizRandom(int count)
        {
            Random random = new Random();
            var quizzes = _context.Quizzes.ToList();
            var quiz = quizzes[random.Next(quizzes.Count)];
            return QuizMappers.FromEntityToQuiz(quiz);
        }

        public IEnumerable<Quiz> FindAllQuizzes()
        {
            return _context
                .Quizzes
                .AsNoTracking()
                .Include(q => q.Items)
                .ThenInclude(i => i.IncorrectAnswers)
                .Select(QuizMappers.FromEntityToQuiz)
                .ToList();
        }

        public Quiz? FindQuizById(int id)
        {
            var entity = _context
                .Quizzes
                .AsNoTracking()
                .Include(q => q.Items)
                .ThenInclude(i => i.IncorrectAnswers)
                .FirstOrDefault(e => e.Id == id);
            return entity is null ? null : QuizMappers.FromEntityToQuiz(entity);
        }

        public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
        {
            var quiz = _context.Quizzes.FirstOrDefault(x => x.Id == quizId);
            if(quiz is null)
            {
                throw new QuizNotFoundException($"Quiz with id {quizId} not found");
            }

            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if(user is null)
            {
                throw new Exception("Uset not found");
            }

            return _context
                .UsersAnswers
                .Where(x => x.UserId == userId && x.QuizId == quizId)
                .Include(x => x.QuizItem)
                .Select(x => new QuizItemUserAnswer(QuizMappers.FromEntityToQuizItem(x.QuizItem), x.UserId, x.QuizId, x.UserAnswer))
                .ToList();
        }

        public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int quizItemId, int userId, string answer)
        {
            QuizItemUserAnswerEntity entity = new QuizItemUserAnswerEntity()
            {
                UserId = userId,
                QuizItemId = quizItemId,
                QuizId = quizId,
                UserAnswer = answer
            };
            try
            {
                var saved = _context.UsersAnswers.Add(entity).Entity;
                _context.SaveChanges();
                //_context.QuizItems.Attach TODO????
                return new QuizItemUserAnswer()
                {
                    UserId = saved.UserId,
                    QuizItem = QuizMappers.FromEntityToQuizItem(saved.QuizItem),
                    QuizId = saved.QuizId,
                    Answer = saved.UserAnswer
                };
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException.Message.StartsWith("The INSERT"))
                {
                    throw new QuizNotFoundException("Quiz, quiz item or user not found. Can't save!");
                }
                if (e.InnerException.Message.StartsWith("Violation of"))
                {
                    throw new QuizAnswerItemAlreadyExistsException(quizId, quizItemId, userId);
                }
                throw new Exception(e.Message);
            }
        }
    }
}
