using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using iKnow.Core.Models;
using iKnow.Core.Repositories;

namespace iKnow.Persistence.Repositories {
    public class QuestionRepository : Repository<Question>, IQuestionRepository {
        private iKnowContext _iKnowContext;
        public QuestionRepository(iKnowContext context) : base(context) {
            _iKnowContext = context;
        }

        public IDictionary<Question, int> GetQuestionsWithAnswerCount(IEnumerable<Question> questions, int? maxNumber) {
            var query = questions.GroupJoin(_iKnowContext.Answers,
                q => q.Id,
                a => a.QuestionId,
                (question, answers) =>
                    new {
                        Question = question,
                        AnswerCount = answers.Count()
                    }).OrderBy(a => Guid.NewGuid());

            if (maxNumber.HasValue) {
                return query.Take(maxNumber.Value).ToDictionary(a => a.Question, a => a.AnswerCount);
            }

            return query.ToDictionary(a => a.Question, a => a.AnswerCount);
        }

    }
}