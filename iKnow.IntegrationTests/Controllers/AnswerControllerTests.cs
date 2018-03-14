using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using iKnow.Controllers;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.IntegrationTests.Extensions;
using iKnow.Persistence;
using Moq;
using NUnit.Framework;

namespace iKnow.IntegrationTests.Controllers {
    [TestFixture]
    public class AnswerControllerTests {
        private AnswerController _controller;
        private iKnowContext _context;
        private Mock<IPrincipal> _currentUser;
        private AppUser _firstUserInDb;

        [SetUp]
        public void Setup() {
            _context = new iKnowContext();
            _controller = new AnswerController(new UnitOfWork(_context));

            _currentUser = new Mock<IPrincipal>();
            _controller.MockContext(new Mock<HttpRequestBase>(), _currentUser);

            _firstUserInDb = _context.Users.First();
            _currentUser.MockIdentity(_firstUserInDb.Id);
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Index_WhenCalled_ShouldReturnQuestionWithAnswerCountInViewModel() {
            var question1 = _context.AddTestQuestionToDatabase("Question 1");
            var question2 = _context.AddTestQuestionToDatabase("Question 2");

            var answer = _context.AddTestAnswerToDatabase(question1.Id);

            var result = _controller.Index();

            var viewModel = (result as ViewResult).Model as QuestionAnswerCountViewModel;
            Assert.That(viewModel.QuestionsWithAnswerCount.Count, Is.EqualTo(2));
            Assert.That(viewModel.QuestionsWithAnswerCount.Single(kvp => kvp.Key.Id == question1.Id).Value, Is.EqualTo(1));
            Assert.That(viewModel.QuestionsWithAnswerCount.Single(kvp => kvp.Key.Id == question2.Id).Value, Is.EqualTo(0));
        }

        [Test, Isolated]
        public void LoadMore_WhenCalled_ShouldReturnQuestionWithAnswerCountInViewModel() {
            var question = _context.AddTestQuestionToDatabase();
            var answer = _context.AddTestAnswerToDatabase(question.Id);

            for (var i = 0; i < Constants.DefaultPageSize; i++) {
                _context.AddTestQuestionToDatabase("First page question" + i);
            }

            var result = _controller.LoadMore(0);

            var dictionary = result.Model as IDictionary<Question, int>;
            Assert.That(dictionary.Single().Key.Id, Is.EqualTo(question.Id));
            Assert.That(dictionary.Single().Value, Is.EqualTo(1));
        }

        [Test, Isolated]
        public void Detail_WhenCalled_ShouldReturnAnswerAndAnswerCountAndQuestionInViewModel() {
            var question = _context.AddTestQuestionToDatabase();
            var answer = _context.AddTestAnswerToDatabase(question.Id);

            var result = _controller.Detail(answer.Id);

            var answerDetailViewModel = (result as ViewResult).Model as AnswerDetailViewModel;
            var questionDetailViewModel = answerDetailViewModel.QuestionDetailViewModel;

            Assert.That(answerDetailViewModel.Answer.Id, Is.EqualTo(answer.Id));
            Assert.That(answerDetailViewModel.AnswerCount, Is.EqualTo(1));
            Assert.That(questionDetailViewModel.Question.Id, Is.EqualTo(question.Id));
        }

        [Test, Isolated]
        public void Detail_UserHasExistingAnswer_ShouldReturnUserAnswerIdAndUserCanDeleteAnswer() {
            var question = _context.AddTestQuestionToDatabase();
            var answer = _context.AddTestAnswerToDatabase(question.Id);

            var result = _controller.Detail(answer.Id);

            var questionDetailViewModel = ((result as ViewResult).Model as AnswerDetailViewModel).QuestionDetailViewModel;

            Assert.That(questionDetailViewModel.UserAnswerId, Is.EqualTo(answer.Id));
            Assert.That(questionDetailViewModel.CanUserDeleteAnswerPanelAnswer, Is.True);
        }

        [Test, Isolated]
        public void Detail_UserHasNotExistingAnswer_ShouldReturnZeroAsUserAnswerIdAndUserCannotDeleteAnswer() {
            var question = _context.AddTestQuestionToDatabase();
            var answer = _context.AddTestAnswerToDatabase(question.Id);
            var user2 = _context.Users.ToList().Last();
            
            answer.AppUserId = user2.Id;

            _context.SaveChanges();

            var result = _controller.Detail(answer.Id);

            var questionDetailViewModel = ((result as ViewResult).Model as AnswerDetailViewModel).QuestionDetailViewModel;

            Assert.That(questionDetailViewModel.UserAnswerId, Is.EqualTo(0));
            Assert.That(questionDetailViewModel.CanUserDeleteAnswerPanelAnswer, Is.False);
        }

        [Test, Isolated]
        public void Save_ExistingAnswer_ShouldUpdateExistingAnswerInDatabase() {
            var question = _context.AddTestQuestionToDatabase();
            var answer = _context.AddTestAnswerToDatabase(question.Id, "original answer");

            var viewModel = new QuestionDetailViewModel {
                Question = question,
                AnswerPanelContent = "new answer"
            };

            _controller.Save(viewModel);

            var answers = _context.Answers.ToList();

            Assert.That(answers.Count, Is.EqualTo(1));
            Assert.That(answers.First().Content, Is.EqualTo(viewModel.AnswerPanelContent));
            Assert.That(answers.First().UpdatedDate.Value.Date, Is.EqualTo(DateTime.Today));
        }

        [Test, Isolated]
        public void Save_NewAnswer_ShouldSaveNewAnswerToDatabase() {
            var question = _context.AddTestQuestionToDatabase();

            var viewModel = new QuestionDetailViewModel {
                Question = question,
                AnswerPanelContent = "new answer"
            };

            _controller.Save(viewModel);

            var answers = _context.Answers.ToList();

            Assert.That(answers.Count, Is.EqualTo(1));
            Assert.That(answers.First().Content, Is.EqualTo(viewModel.AnswerPanelContent));
            Assert.That(answers.First().CreatedDate.Date, Is.EqualTo(DateTime.Today));
            Assert.That(answers.First().UpdatedDate, Is.Null);
            Assert.That(answers.First().AppUserId, Is.EqualTo(_firstUserInDb.Id));
        }

        [Test, Isolated]
        public void EditIcon_UserIsAnswerOwner_ShouldReturnPartialViewResult() {
            var question = _context.AddTestQuestionToDatabase();
            var answer = _context.AddTestAnswerToDatabase(question.Id);
            
            var result = _controller.EditIcon(answer.Id);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test, Isolated]
        public void GetAnswerPanelHeader_WhenCalled_ShouldReturnUserWithGivenIdInViewModel() {
            var result = _controller.GetAnswerPanelHeader(_firstUserInDb.Id);

            Assert.That((result.Model as AppUser).Id, Is.EqualTo(_firstUserInDb.Id));
        }

        [Test, Isolated]
        public void Delete_WhenCalled_ShouldRemoveAnswerFromDatabase() {
            var question = _context.AddTestQuestionToDatabase();
            var answer = _context.AddTestAnswerToDatabase(question.Id);

            var viewModel = new QuestionDetailViewModel {
                Question = question,
                UserAnswerId = answer.Id
            };

            _controller.Delete(viewModel);

            Assert.That(_context.Answers.Count(), Is.EqualTo(0));
        }
    }
}
