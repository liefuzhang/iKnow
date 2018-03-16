using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public IEnumerable<Question> GetQuestionsOrderByDescending(Func<IQueryable<Question>, IOrderedQueryable<Question>> orderByDesc,
            string includeProperties = null, int? skip = null, int? take = null) {
            includeProperties = includeProperties ?? "";

            IQueryable<Question> query = _iKnowContext.Questions;
            query = orderByDesc(query);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                query = query.Include(includeProperty);
            }
            
            if (take.HasValue) {
                return query.Skip(skip ?? 0).Take(take.Value).ToList();
            }

            return query.Skip(skip ?? 0).ToList();
        }
    }
}