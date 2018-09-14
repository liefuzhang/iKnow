using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using iKnow.Core.Models;
using iKnow.Core.Repositories;

namespace iKnow.Persistence.Repositories {
    public class AnswerRepository : Repository<Answer>, IAnswerRepository {
        private iKnowContext _iKnowContext;

        public AnswerRepository(iKnowContext context) : base(context) {
            _iKnowContext = context;
        }

        //TODO should we have this in a provider class? As it's not returning Answer collection
        public IDictionary<Question, Answer> GetQuestionAnswerPairsForGivenQuestions(List<int> questionIds,
            string currentUserId) {
            if (questionIds == null) {
                return null;
            }

            var answers = _iKnowContext.Answers
                .Where(a => questionIds.Contains(a.QuestionId))
                .GroupBy(a => a.QuestionId, (qId, g) => new {
                    QuestionId = qId,
                    Answer = g.OrderBy(a => Guid.NewGuid()).FirstOrDefault()
                })
                .OrderBy(a => Guid.NewGuid())
                .ToList();

            var answerIds = answers.Select(a => a.Answer.Id);
            _iKnowContext.Answers.Where(a => answerIds.Contains(a.Id))
                .Include(a => a.Question)
                .Include(a => a.Comments)
                .Include(a => a.AnswerLikes)
                .Include(a => a.AppUser)
                .Load();

            var questionAnswers = new Dictionary<Question, Answer>();
            foreach (var answer in answers) {
                answer.Answer.SetLikedByCurrentUser(currentUserId);
                questionAnswers.Add(answer.Answer.Question, answer.Answer);
            }
            return questionAnswers;
        }

        public new void RemoveRange(IEnumerable<Answer> answers) {
            foreach (var answer in answers) {
                answer.IsDeleted = true;
            }
        }

        public new void Remove(Answer answer) {
            answer.IsDeleted = true;
        }

        protected override IQueryable<Answer> GetQueryable(Expression<Func<Answer, bool>> filter = null,
            Func<IQueryable<Answer>, IOrderedQueryable<Answer>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            Expression<Func<Answer, bool>> anotherFilter = null) {
            anotherFilter = answer => !answer.IsDeleted;
            return base.GetQueryable(filter, orderBy, includeProperties, skip, take, anotherFilter);
        }
    }
}