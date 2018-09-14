using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using iKnow.Core.Models;
using iKnow.Core.Repositories;

namespace iKnow.Persistence.Repositories {
    public class QuestionRepository : Repository<Question>, IQuestionRepository {
        private readonly iKnowContext _iKnowContext;
        public QuestionRepository(iKnowContext context) : base(context) {
            _iKnowContext = context;
        }

        public IDictionary<Question, int> GetQuestionsWithAnswerCount(IEnumerable<Question> questions) {
            var query = questions?.GroupJoin(_iKnowContext.Answers,
                q => q.Id,
                a => a.QuestionId,
                (question, answers) =>
                    new {
                        Question = question,
                        AnswerCount = answers.Count()
                    }).OrderBy(a => Guid.NewGuid());

            return query?.ToDictionary(a => a.Question, a => a.AnswerCount);
        }

        public new void RemoveRange(IEnumerable<Question> questions) {
            foreach (var question in questions) {
                question.IsDeleted = true;
            }
        }

        public new void Remove(Question question) {
            question.IsDeleted = true;
        }

        protected override IQueryable<Question> GetQueryable(Expression<Func<Question, bool>> filter = null,
            Func<IQueryable<Question>, IOrderedQueryable<Question>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            Expression<Func<Question, bool>> anotherFilter = null) {
            anotherFilter = question => !question.IsDeleted;
            return base.GetQueryable(filter, orderBy, includeProperties, skip, take, anotherFilter);
        }
    }
}