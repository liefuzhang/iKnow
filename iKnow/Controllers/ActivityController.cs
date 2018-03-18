using System.Web.Mvc;
using iKnow.Core;
using iKnow.Core.ViewModels;
using iKnow.Persistence;

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
    }
}
