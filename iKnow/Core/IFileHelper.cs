using System.Web;
using iKnow.Core.Models;

namespace iKnow.Core {
    public interface IFileHelper {
        void SaveTopicIcon(HttpPostedFileBase postedFile, string topicName);
        void SaveUserIcon(string dataURL, string userId);
        bool DoesFileExist(string path);
    }
}