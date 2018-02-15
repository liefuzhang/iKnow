using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.Hosting;
using iKnow.Core;
using iKnow.Core.Models;

namespace iKnow.Persistence {
    public class ImageFileGenerator : IImageFileGenerator {
        public void SaveTopicIcon(HttpPostedFileBase postedFile, Topic topic) {
            if (postedFile != null && postedFile.ContentLength > 0) {
                var bitmap = Image.FromStream(postedFile.InputStream);
                var scale = Math.Max(bitmap.Width / Constants.TopicIconDefaultSize,
                    bitmap.Height / Constants.TopicIconDefaultSize);
                var resized = new Bitmap(bitmap,
                    new Size(Convert.ToInt32(bitmap.Width / scale), Convert.ToInt32(bitmap.Height / scale)));

                var iconFolder = HostingEnvironment.MapPath(Constants.TopicIconFolderPath);
                var fileName = topic.Name.ToLower().Replace(' ', '-') + ".png";
                resized.Save(iconFolder + fileName, ImageFormat.Png);
            }
        }

        public void SaveUserIcon(HttpPostedFileBase postedPhoto, AppUser user) {
            // save icon if it exists
            if (postedPhoto != null && postedPhoto.ContentLength > 0) {
                var bitmap = Bitmap.FromStream(postedPhoto.InputStream);
                var scale = Math.Max(bitmap.Width / Constants.UserIconDefaultSize,
                    bitmap.Height / Constants.UserIconDefaultSize);
                var resized = new Bitmap(bitmap,
                    new Size(Convert.ToInt32(bitmap.Width/scale), Convert.ToInt32(bitmap.Height/scale)));

                var iconFolder = HostingEnvironment.MapPath(Constants.UserIconFolderPath);
                var fileName = user.Id.ToLower().Replace(' ', '-') + ".png";
                resized.Save(iconFolder + fileName, ImageFormat.Png);
            }
        }
    }
}