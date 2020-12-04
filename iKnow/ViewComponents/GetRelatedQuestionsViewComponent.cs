using System.Collections.Generic;
using System.Linq;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace iKnow.ViewComponents
{
    public class GetRelatedQuestionsViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRelatedQuestionsViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke(int id)
        {
            var currentQuestion = _unitOfWork.QuestionRepository.Single(q => q.Id == id, nameof(Question.TopicQuestions));
            var topicIds = currentQuestion.TopicQuestions.Select(tq => tq.TopicId);
            const int relatedQuestionMaxNumber = 5;
            var relatedQuestions = _unitOfWork.QuestionRepository.Get(q =>
                    q.Id != id && q.TopicQuestions.Any(tq => topicIds.Contains(tq.TopicId)),
                take: relatedQuestionMaxNumber).ToList();

            if (!relatedQuestions.Any())
            {
                return Content(string.Empty);
            }

            var viewModel = ConstructQuestionAnswerCountViewModel(relatedQuestions);

            return View(viewModel);
        }

        private QuestionAnswerCountViewModel ConstructQuestionAnswerCountViewModel(IEnumerable<Question> relatedQuestions)
        {
            var questionsWithAnswerCount = _unitOfWork.QuestionRepository.GetQuestionsWithAnswerCount(relatedQuestions);

            var viewModel = new QuestionAnswerCountViewModel
            {
                QuestionsWithAnswerCount = questionsWithAnswerCount
            };
            return viewModel;
        }
    }
}