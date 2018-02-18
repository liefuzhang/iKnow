using System.Web;
using iKnow.Core.Models;

namespace iKnow.Core {
    public interface IImageFileGenerator {
        void SaveTopicIcon(HttpPostedFileBase postedFile, string topicName);
        void SaveUserIcon(HttpPostedFileBase postedFile, string userId);
    }
}