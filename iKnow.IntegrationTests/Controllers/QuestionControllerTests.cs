using System.Collections;
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
using System.Data.Entity;
using NUnit.Framework;

namespace iKnow.IntegrationTests.Controllers {
    [TestFixture]
    public class QuestionControllerTests {
        private QuestionController _controller;
        private iKnowContext _context;
        private Mock<IPrincipal> _user;

        [SetUp]
        public void Setup() {
            _context = new iKnowContext();
            _controller = new QuestionController(new UnitOfWork(_context));

            _user = new Mock<IPrincipal>();
            _controller.MockContext(new Mock<HttpRequestBase>(), _user);

            var user1 = _context.Users.First();
            _user.MockIdentity(user1.Id);
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }

        [Test, Isolated]
        public void GetForm_IdIsNotNull_ShouldReturnQuestionWithGivenIdAndWithItsTopicsAndAllTopics() {
            var topic1 = _context.AddTestTopicToDatabase();
            var topic2 = _context.AddTestTopicToDatabase();
            var question = _context.AddTestQuestionToDatabase();
            question.AddTopic(topic1);

            _context.SaveChanges();

            var result = _controller.GetForm(question.Id);

            Assert.That((result.Model as QuestionFormViewModel).Question.Id, Is.EqualTo(question.Id));
            Assert.That((result.Model as QuestionFormViewModel).Question.Topics.First().Id, Is.EqualTo(topic1.Id));
            Assert.That(((result.Model as QuestionFormViewModel).Topics.Items as ICollection).Count, Is.EqualTo(2));
        }

        [Test, Isolated]
        public void GetForm_IdIsNull_ShouldReturnNewQuestionAndAllTopics() {
            var topic1 = _context.AddTestTopicToDatabase();
            var topic2 = _context.AddTestTopicToDatabase();

            var result = _controller.GetForm(null);

            Assert.That((result.Model as QuestionFormViewModel).Question, Is.Not.Null);
            Assert.That((result.Model as QuestionFormViewModel).Question.Topics.Count, Is.EqualTo(0));
            Assert.That(((result.Model as QuestionFormViewModel).Topics.Items as ICollection).Count, Is.EqualTo(2));
        }

        [Test, Isolated]
        public void GetTopic_WhenCalled_ShouldReturnQuestionWithGivenIdAndWithItsTopicsAndAllTopics() {
            var topic1 = _context.AddTestTopicToDatabase();
            var topic2 = _context.AddTestTopicToDatabase();
            var question = _context.AddTestQuestionToDatabase();
            question.AddTopic(topic1);

            _context.SaveChanges();

            var result = _controller.GetTopic(question.Id);

            Assert.That((result.Model as QuestionFormViewModel).Question.Id, Is.EqualTo(question.Id));
            Assert.That((result.Model as QuestionFormViewModel).Question.Topics.First().Id, Is.EqualTo(topic1.Id));
            Assert.That(((result.Model as QuestionFormViewModel).Topics.Items as ICollection).Count, Is.EqualTo(2));
        }

        [Test, Isolated]
        public void Detail_WhenCalled_ShouldReturnQuestionWithGivenId() {
            var question = _context.AddTestQuestionToDatabase();

            var result = _controller.Detail(question.Id);

            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).Question.Id, Is.EqualTo(question.Id));
        }


        [Test, Isolated]
        public void Detail_UserHasExistingAnswer_ShouldReturnUserAnswerIdAndUserCanDeleteAnswer() {
            var question = _context.AddTestQuestionToDatabase();
            var answer = _context.AddTestAnswerToDatabase(question.Id);

            var result = _controller.Detail(question.Id);

            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).CanUserDeleteAnswerPanelAnswer, Is.True);
            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).UserAnswerId, Is.EqualTo(answer.Id));
        }

        [Test, Isolated]
        public void Detail_UserHasNotExistingAnswer_ShouldReturnZeroAsUserAnswerIdAndUserCannotDeleteAnswer() {
            var question = _context.AddTestQuestionToDatabase();

            var result = _controller.Detail(question.Id);

            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).CanUserDeleteAnswerPanelAnswer, Is.False);
            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).UserAnswerId, Is.EqualTo(0));
        }

        [Test, Isolated]
        public void Save_NewQuestion_ShouldSaveQuestionAndItsTopicsToDatabase() {
            var topic = _context.AddTestTopicToDatabase();

            var question = new Question {
                Title = "New Question?",
            };

            var viewModel = new QuestionFormViewModel {
                Question = question,
                TopicIds = new[] { topic.Id }
            };

            var result = _controller.Save(viewModel);

            var questionInDb = _context.Questions.Include(q => q.Topics).SingleOrDefault();

            Assert.That(questionInDb, Is.Not.Null);
            Assert.That(questionInDb.Topics.Count, Is.EqualTo(1));
        }

        [Test, Isolated]
        public void Save_NewQuestionWithExistingTitle_ShouldNotSaveQuestion() {
            var topic = _context.AddTestTopicToDatabase();
            var existingQuestion = _context.AddTestQuestionToDatabase();

            var question = new Question {
                Title = existingQuestion.Title,
            };

            var viewModel = new QuestionFormViewModel {
                Question = question,
                TopicIds = new[] { topic.Id }
            };

            var result = _controller.Save(viewModel);

            var questionCount = _context.Questions.Count();

            Assert.That(questionCount, Is.EqualTo(1));
        }

        [Test, Isolated]
        public void Save_ExistingQuestion_ShouldUpdateQuestionAndItsTopicsInDatabase() {
            var topic = _context.AddTestTopicToDatabase();
            var existingQuestion = _context.AddTestQuestionToDatabase();

            var title = existingQuestion.Title;
            var description = existingQuestion.Description;

            var question = new Question {
                Title = title + "?",
                Description = description + "-",
                Id = existingQuestion.Id,
            };
            question.SetUserId(existingQuestion.AppUserId);

            var viewModel = new QuestionFormViewModel {
                Question = question,
                TopicIds = new[] { topic.Id }
            };

            var result = _controller.Save(viewModel);

            var questionInDb = _context.Questions.Include(q => q.Topics).SingleOrDefault();

            Assert.That(questionInDb.Title, Is.EqualTo(title + "?"));
            Assert.That(questionInDb.Description, Is.EqualTo(description + "-"));
            Assert.That(questionInDb.Topics.Count, Is.EqualTo(1));
        }

        [Test, Isolated]
        public void SaveQuestionTopics_WhenCalled_ShouldUpdateQuestionTopicsInDatabase() {

        }

    }
}
