using System.Web;
using iKnow.Core.Models;

namespace iKnow.Core {
    public interface IImageFileGenerator {
        void SaveTopicIcon(HttpPostedFileBase postedFile, Topic topic);
    }
}