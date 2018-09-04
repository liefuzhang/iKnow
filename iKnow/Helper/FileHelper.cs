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
        public void SaveTopicIcon(HttpPostedFileBase postedFile, string topicName) {
            if (postedFile != null && postedFile.ContentLength > 0) {
                var bitmap = Image.FromStream(postedFile.InputStream);
                var scale = Math.Max(bitmap.Width / Constants.TopicIconDefaultSize,
                    bitmap.Height / Constants.TopicIconDefaultSize);
                var resized = new Bitmap(bitmap,
                    new Size(Convert.ToInt32(bitmap.Width / scale), Convert.ToInt32(bitmap.Height / scale)));

                var iconFolder = HostingEnvironment.MapPath(Constants.TopicIconFolderPath);
                var fileName = topicName.ToLower().Replace(' ', '-') + Constants.DefaultIconExtension;
                resized.Save(iconFolder + fileName, ImageFormat.Png);
            }
        }

        public void SaveUserIcon(string dataURL, string userId) {
            if (!string.IsNullOrEmpty(dataURL)) {
                var search = ";base64,";
                var match = dataURL.IndexOf(search, StringComparison.Ordinal);
                if (match > 0) {
                    dataURL = dataURL.Substring(match + search.Length);
                }
                var iconFolder = HostingEnvironment.MapPath(Constants.UserIconFolderPath);
                var fileName = userId.ToLower().Replace(' ', '-') + Constants.DefaultIconExtension;
                File.WriteAllBytes(iconFolder + fileName, Convert.FromBase64String(dataURL));
            }
        }

        public bool DoesFileExist(string path) {
            return File.Exists(path);
        }
    }
}