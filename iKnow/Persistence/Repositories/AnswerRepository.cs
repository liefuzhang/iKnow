using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using iKnow.Core.Models;
using iKnow.Core.Repositories;

namespace iKnow.Persistence.Repositories {
    public class AnswerRepository : Repository<Answer>, IAnswerRepository {
        private iKnowContext _iKnowContext;

        public AnswerRepository(iKnowContext context) : base(context) {
            _iKnowContext = context;
        }
        
        //TODO should we have this in a provider class? As it's not returning Answer collection
        public IDictionary<Question, Answer> GetQuestionAnswerPairsForGivenQuestions(List<int> questionIds) {
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

            var questionAnswers = new Dictionary<Question, Answer>();
            foreach (var answer in answers) {
                var question = _iKnowContext.Questions.Single(q => q.Id == answer.QuestionId);
                _iKnowContext.Users.Where(u => u.Id == answer.Answer.AppUserId).Load();
                questionAnswers.Add(question, answer.Answer);
            }
            return questionAnswers;
        }
    }
}