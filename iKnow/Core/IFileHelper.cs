using System.Web;
using iKnow.Core.Models;

namespace iKnow.Core {
    public interface IFileHelper {
        void SaveTopicIcon(HttpPostedFileBase postedFile, string topicName);
        void SaveUserIcon(HttpPostedFileBase postedFile, string userId);
        bool DoesFileExist(string path);
    }
}