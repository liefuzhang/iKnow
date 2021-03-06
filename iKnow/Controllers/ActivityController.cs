﻿using System.Web.Mvc;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.Persistence;
using Microsoft.AspNet.Identity;

namespace iKnow.Controllers {
    public class ActivityController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public ActivityController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public ActivityController() {
            _unitOfWork = new UnitOfWork();
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        public PartialViewResult GetFollowTopic(int id) {
            var activity = _unitOfWork.ActivityRepository.Single(a => a.Id == id);
            var topic = _unitOfWork.TopicRepository.Single(t => t.Id == activity.TopicId);

            var viewModel = new ActivityViewModel {
                DateTime = activity.DateTime,
                Topic = topic
            };

            return PartialView("_ActivityFollowTopicPartial", viewModel);
        }

        private ActivityViewModel GetQuestionAnswerViewModel(int id)
        {
            var activity = _unitOfWork.ActivityRepository.Single(a => a.Id == id);
            var question = _unitOfWork.QuestionRepository.Single(q => q.Id == activity.QuestionId);
            var answer = _unitOfWork.AnswerRepository.Single(a => a.Id == activity.AnswerId,
                nameof(Answer.AppUser) + "," + nameof(Answer.AnswerLikes) + "," + nameof(Answer.Comments));
            answer.SetLikedByCurrentUser(User.Identity.GetUserId());

            var viewModel = new ActivityViewModel
            {
                DateTime = activity.DateTime,
                Question = question,
                Answer = answer
            };
            return viewModel;
        }

        public PartialViewResult GetAnswerQuestion(int id)
        {
            var viewModel = GetQuestionAnswerViewModel(id);

            return PartialView("_ActivityAnswerQuestionPartial", viewModel);
        }

        public PartialViewResult GetLikeAnswer(int id)
        {
            var viewModel = GetQuestionAnswerViewModel(id);

            return PartialView("_ActivityLikeAnswerPartial", viewModel);
        }

        public PartialViewResult GetAddQuestion(int id) {
            var activity = _unitOfWork.ActivityRepository.Single(a => a.Id == id);
            var question = _unitOfWork.QuestionRepository.Single(q => q.Id == activity.QuestionId);

            var viewModel = new ActivityViewModel {
                DateTime = activity.DateTime,
                Question = question
            };

            return PartialView("_ActivityAddQuestionPartial", viewModel);
        }
    }
}
