using System;
using System.IO;

namespace iKnow.Helper
{
    public static class ServerHelper
    {
        public static string MapPath(string path)
        {
            return Path.Combine(
                (string)AppDomain.CurrentDomain.GetData("WebRootPath"), path);
        }
    }
}
