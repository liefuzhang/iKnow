using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.Helper;
using iKnow.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Constants = iKnow.Core.Models.Constants;

namespace iKnow.Controllers
{
    public class TopicController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileHelper _fileHelper;

        public TopicController(IUnitOfWork unitOfWork, IFileHelper fileHelper)
        {
            _unitOfWork = unitOfWork;
            _fileHelper = fileHelper;
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        // GET: Topic
        public ViewResult Index()
        {
            var topics = _unitOfWork.TopicRepository.GetAll().ToList();
            Topic selectedTopic = null;
            if (topics.Any())
            {
                if (Request.Query["selectedTopicId"].ToString() != null)
                {
                    selectedTopic = topics.SingleOrDefault(t => t.Id.ToString() == Request.Query["selectedTopicId"].ToString());
                }
                if (selectedTopic == null)
                {
                    selectedTopic = topics.First();
                }
            }
            var viewModel = new TopicIndexViewModel
            {
                Topics = topics,
                SelectedTopic = selectedTopic
            };

            return View(viewModel);
        }

        // GET: Topic/Detail/1
        public ActionResult Detail(int id)
        {
            var topic = _unitOfWork.TopicRepository.SingleOrDefault(t => t.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = new TopicDetailViewModel
            {
                Topic = topic,
                QuestionAnswers = GetQuestionAnswerPairsForTopic(topic.Id, 0),
                IsFollowing = _unitOfWork.TopicFollowingRepository
                    .Any(f => f.TopicId == topic.Id && f.UserId == userId)
            };

            return View(viewModel);
        }

        private IDictionary<Question, Answer> GetQuestionAnswerPairsForTopic(int topicId, int currentPage, int pageSize = Constants.DefaultPageSize)
        {
            var questions = _unitOfWork.QuestionRepository.Get(q => q.TopicQuestions.Any(tq => tq.TopicId == topicId) && q.Answers.Any(),
                query => query.OrderByDescending(question => question.Id), null, currentPage * pageSize, pageSize).ToList();
            var questionIds = questions.Select(q => q.Id).ToList();
            var questionAnswers = _unitOfWork.AnswerRepository
                .GetQuestionAnswerPairsForGivenQuestions(questionIds, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return questionAnswers;
        }

        [Route("Topic/LoadMore/{currentPage}")]
        public IActionResult LoadMore(int currentPage, int topicId)
        {
            var topic = _unitOfWork.TopicRepository.SingleOrDefault(t => t.Id == topicId);
            if (topic == null)
            {
                return Content(string.Empty);
            }

            var pairs = GetQuestionAnswerPairsForTopic(topic.Id, ++currentPage);
            if (pairs == null || !pairs.Any())
            {
                return Content(string.Empty);
            }

            return PartialView("_QuestionAnswerPairPartial", pairs);
        }


        // GET: Topic/About/1
        public IActionResult About(int id)
        {
            var topic = _unitOfWork.TopicRepository.SingleOrDefault(t => t.Id == id);
            if (topic == null)
            {
                return Content(string.Empty);
            }

            return PartialView("_TopicBodyPartial", topic);
        }

        // GET: Topic/Add
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Add()
        {
            var topic = new Topic();
            var viewModel = new TopicFormViewModel
            {
                Topic = topic
            };

            return View("TopicForm", viewModel);
        }

        // POST: Topic/Save
        [HttpPost]
        [Authorize(Roles = Constants.AdminRoleName)]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TopicFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("TopicForm", viewModel);
            }

            var topic = viewModel.Topic;
            topic.TrimNameAndDescription();

            // check if topic name is unique 
            if (DoesTopicNameExist(topic))
            {
                ModelState.AddModelError("", "Topic already exists.");
                return View("TopicForm", viewModel);
            }

            try
            {
                SaveTopic(topic);

                _fileHelper.SaveTopicIcon(viewModel.PostedFile, topic);

                return RedirectToAction("Index", new { selectedTopicId = topic.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("TopicForm", viewModel);
            }
        }

        private void SaveTopic(Topic topic)
        {
            if (topic.Id == 0)
            {
                _unitOfWork.TopicRepository.Add(topic);
            }
            else
            {
                var topicInDb = _unitOfWork.TopicRepository.Single(t => t.Id == topic.Id);
                topicInDb.UpdateNameAndDescription(topic.Name, topic.Description);
            }

            _unitOfWork.Complete();
        }

        private bool DoesTopicNameExist(Topic topic)
        {
            return topic.Id == 0 && _unitOfWork.TopicRepository.Any(q => q.Name == topic.Name);
        }

        // GET: Topic/Edit/1
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(int id)
        {
            var topic = _unitOfWork.TopicRepository.SingleOrDefault(t => t.Id == id);
            if (topic == null)
            {
                return NotFound();
            }

            var viewModel = new TopicFormViewModel
            {
                Topic = topic
            };

            return View("TopicForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Delete(Topic topic)
        {
            var topicInDb = _unitOfWork.TopicRepository.SingleOrDefault(t => t.Id == topic.Id);
            if (topicInDb == null)
            {
                return NotFound();
            }

            _unitOfWork.TopicRepository.Remove(topicInDb);
            _unitOfWork.Complete();

            _fileHelper.DeleteTopicIcon(topicInDb.IconSavePathOnServer);

            return RedirectToAction("Index");
        }
    }
}