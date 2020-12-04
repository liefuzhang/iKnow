using System.Web;
using iKnow.Core.Models;
using Microsoft.AspNetCore.Http;

namespace iKnow.Core {
    public interface IFileHelper {
        void SaveTopicIcon(IFormFile postedFile, Topic topic);
        void SaveUserIcon(string dataURL, AppUser user);
        bool DoesFileExist(string path);
        void DeleteTopicIcon(string iconSavePath);
    }
}