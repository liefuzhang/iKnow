using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using iKnow.ViewModels;

namespace iKnow.Controllers {
    public class QuestionController : Controller {
        private iKnowContext _context;

        public QuestionController() {
            _context = new iKnowContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        public ActionResult Detail(int id) {
            var question = _context.Questions.Include("Topics").SingleOrDefault(q => q.Id == id);
            if (question == null) {
                return HttpNotFound();
            }

            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddQuestionViewModel viewModel) {
            if (!ModelState.IsValid) {
                // return to current page
                return Redirect(Request.UrlReferrer.ToString());
            }

            var question = viewModel.Question;
            // TODO  remove
            var topic = _context.Topics.SingleOrDefault(t=>t.Name == viewModel.Topic);
            question.UserId = 1;
            question.AddTopic(topic);

            _context.Questions.Add(question);
            _context.SaveChanges();
            return RedirectToAction("Detail", new { id = question.Id });
        }
    }
}