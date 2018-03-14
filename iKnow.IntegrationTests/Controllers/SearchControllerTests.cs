﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SearchControllerTests {
        private SearchController _controller;
        private iKnowContext _context;

        [SetUp]
        public void Setup() {
            _context = new iKnowContext();
            _controller = new SearchController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown() {
            _context.Dispose();
        }

        [Test, Isolated]
        [TestCase("So", 1, 0)]
        [TestCase("so", 1, 0)]
        [TestCase("top", 2, 0)]
        [TestCase("Ano", 1, 1)]
        [TestCase("que", 0, 2)]
        [TestCase("see", 0, 0)]
        public void GetResult_WhenCalled_ShouldReturnSearchResult(string term, int expectedTopicCount, int expectedQuestionCount) {
            var topic1 = _context.AddTestTopicToDatabase("Some Topic");
            var topic2 = _context.AddTestTopicToDatabase("Another Topic");
            var question1 = _context.AddTestQuestionToDatabase("Test search keyword question?");
            var question2 = _context.AddTestQuestionToDatabase("Another test search keyword question?");

            var answer = _context.AddTestAnswerToDatabase(question1.Id);

            _context.SaveChanges();

            var result = _controller.GetResult(term);

            Assert.That((result.Model as SearchResultViewModel).Topics.Count(), Is.EqualTo(expectedTopicCount));
            Assert.That((result.Model as SearchResultViewModel).QuestionsWithAnswerCount.Count(), Is.EqualTo(expectedQuestionCount));
        }

        [Test, Isolated]
        public void GetResult_WhenCalled_ShouldReturnSearchResultWithQuestionsAndAnswerCount() {
            var question1 = _context.AddTestQuestionToDatabase("Test search keyword question?");
            var question2 = _context.AddTestQuestionToDatabase("Another test search keyword question?");

            var answer = _context.AddTestAnswerToDatabase(question1.Id);

            _context.SaveChanges();

            var result = _controller.GetResult("key");

            Assert.That((result.Model as SearchResultViewModel).QuestionsWithAnswerCount.Count(), Is.EqualTo(2));
            Assert.That((result.Model as SearchResultViewModel).QuestionsWithAnswerCount.Single(kvp => kvp.Key.Id == question1.Id).Value, Is.EqualTo(1));
            Assert.That((result.Model as SearchResultViewModel).QuestionsWithAnswerCount.Single(kvp => kvp.Key.Id == question2.Id).Value, Is.EqualTo(0));
        }
    }
}