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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddQuestionViewModel viewModel) {
            try {
                var question = viewModel.Question;
                _context.Questions.Add(question);
                _context.SaveChanges();
                return View("Detail", "Question");
            } catch (DbEntityValidationException ex) {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
    }
}