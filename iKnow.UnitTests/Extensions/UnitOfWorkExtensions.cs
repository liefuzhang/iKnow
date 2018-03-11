using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using iKnow.Controllers;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.Repositories;
using Moq;

namespace iKnow.UnitTests.Extensions {
    public static class UnitOfWorkExtensions {
        public static void MockRepositories(this Mock<IUnitOfWork> uow) {
            var questionRepository = new Mock<IQuestionRepository>();
            var answerRepository = new Mock<IAnswerRepository>();
            var topicRepository = new Mock<ITopicRepository>();
            var userRepository = new Mock<IUserRepository>();
            uow.SetupGet(u => u.QuestionRepository).Returns(questionRepository.Object);
            uow.SetupGet(u => u.AnswerRepository).Returns(answerRepository.Object);
            uow.SetupGet(u => u.TopicRepository).Returns(topicRepository.Object);
            uow.SetupGet(u => u.UserRepository).Returns(userRepository.Object);

        }
    }
}
