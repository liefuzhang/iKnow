using iKnow.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.Models;

namespace iKnow.Controllers
{
    public class TopicController : Controller
    {
        iKnowContext _context;
        public TopicController() {
            _context = new iKnowContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        // GET: Topic
        public ActionResult Index()
        {
            var topics = _context.Topics.ToList();
            var viewModel = new TopicIndexViewModel {
                Topics = topics,
                SelectedTopic = topics[0]
            };

            return View(viewModel);
        }

        // GET: Topic/Detail/1
        public ActionResult Detail(int id) {

            return View();
        }

        // GET: Topic/About/1
        public PartialViewResult About(int id) {
            var topic = _context.Topics.SingleOrDefault(t => t.Id == id);
            if (topic == null) {
                return null;
            }

            return PartialView("_TopicBodyPartial", topic);
        }

        // GET: Topic/Add
        public ActionResult Add() {
            var topic = new Topic();

            return View(topic);
        }

        // POST: Topic/Save
        [HttpPost]
        public ActionResult Save(Topic topic) {
            if (!ModelState.IsValid) {
                return View("Add");
            }

            topic.Id = 0;
            _context.Topics.Add(topic);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}