using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Hosting;
using iKnow.Core;
using iKnow.Core.Models;

namespace iKnow {
    public class FileHelper : IFileHelper {
        public void SaveTopicIcon(HttpPostedFileBase postedFile, Topic topic) {
            if (topic == null)
                return;

            if (postedFile != null && postedFile.ContentLength > 0) {
                var bitmap = Image.FromStream(postedFile.InputStream);
                var scale = Math.Max(bitmap.Width / Constants.TopicIconDefaultSize,
                    bitmap.Height / Constants.TopicIconDefaultSize);
                var resized = new Bitmap(bitmap,
                    new Size(Convert.ToInt32(bitmap.Width / scale), Convert.ToInt32(bitmap.Height / scale)));

                resized.Save(topic.IconSavePathOnServer, ImageFormat.Png);
            }
        }
        public void DeleteTopicIcon(string iconSavePath) {
            if (DoesFileExist(iconSavePath))
                File.Delete(iconSavePath);
        }

        public void SaveUserIcon(string dataURL, AppUser user) {
            if (!string.IsNullOrEmpty(dataURL)) {
                var search = ";base64,";
                var match = dataURL.IndexOf(search, StringComparison.Ordinal);
                if (match > 0) {
                    dataURL = dataURL.Substring(match + search.Length);
                }
                File.WriteAllBytes(user.IconSavePathOnServer, Convert.FromBase64String(dataURL));
            }
        }

        public bool DoesFileExist(string path) {
            return File.Exists(path);
        }
    }
}