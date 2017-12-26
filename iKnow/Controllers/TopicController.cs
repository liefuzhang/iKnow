using iKnow.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}