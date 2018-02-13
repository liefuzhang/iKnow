using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using iKnow.Core.Models;

namespace iKnow.Core.Repositories {
    public interface IQuestionRepository : IRepository<Question> {
        IDictionary<Question, int> GetQuestionsWithAnswerCount(IEnumerable<Question> questions);

        IEnumerable<Question> GetQuestionsOrdeyByDescending(
            Func<IQueryable<Question>, IOrderedQueryable<Question>> orderByDesc, int? skip, int? take);
    }
}