using System.Web;
using iKnow.Core.Models;

namespace iKnow.Core {
    public interface IFileHelper {
        void SaveTopicIcon(HttpPostedFileBase postedFile, Topic topic);
        void SaveUserIcon(string dataURL, AppUser user);
        bool DoesFileExist(string path);
        void DeleteTopicIcon(string iconSavePath);
    }
}