using Junior.Core.Extension;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Junior.Core.Service.Static
{
    public static class FileService
    {
        /// <summary>
        /// 下载文件（异步）
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="strSavePath"></param>
        public static async void DownloadFileAsync(string strUrl, string strSavePath)
        {
            if (File.Exists(strSavePath))
            {
                File.Delete(strSavePath);
            }
            using (var web = new WebClient())
            {
                await web.DownloadFileTaskAsync(strUrl, strSavePath);
            }
        }
        /// <summary>
        /// 下载文件（同步）
        /// </summary>
        /// <param name="strUrl"></param>
        /// <param name="strSavePath"></param>
        public static void DownloadFile(string strUrl, string strSavePath)
        {
            if (File.Exists(strSavePath))
            {
                File.Delete(strSavePath);
            }
            using (var web = new WebClient())
            {
                web.DownloadFile(strUrl, strSavePath);
            }
        }
        /// <summary>
        /// 下载文件（不知道文件名）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <param name="overwrite"></param>
        /// <param name="fileName"></param>
        /// <param name="callback"></param>
        public static void HttpDownloadFile(string url, string path, bool overwrite, out string fileName, Action<string, long, long> callback = null)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //获取文件名
            fileName = response.Headers["Content-Disposition"];//attachment;filename=FileName.txt
            if (string.IsNullOrEmpty(fileName))
                fileName = response.ResponseUri.Segments[response.ResponseUri.Segments.Length - 1];
            else
                fileName = fileName.Remove(0, fileName.IndexOf("filename=") + 9);
            fileName = fileName.Replace(" ", "");
            fileName = fileName.Replace("\"", "");
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            using (Stream responseStream = response.GetResponseStream())
            {
                long totalLength = response.ContentLength;
                //创建本地文件写入流
                using (Stream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    byte[] bArr = new byte[1024];
                    int size;
                    while ((size = responseStream.Read(bArr, 0, bArr.Length)) > 0)
                    {
                        stream.Write(bArr, 0, size);
                        callback?.Invoke(fileName, totalLength, stream.Length);
                    }
                }
            }
        }
        public static string GetWebSourceCode(string strUrl, string strReferer = "")
        {
            WebClient myWebClient = new WebClient();
            if (!strReferer.IsNull())
            {
                myWebClient.Headers.Add(HttpRequestHeader.Referer, strReferer);
            }
            Stream myStream = myWebClient.OpenRead(strUrl);
            StreamReader sr = new StreamReader(myStream, Encoding.GetEncoding("utf-8"));
            string strHTML = sr.ReadToEnd();
            myStream.Close();
            strHTML = Regex.Replace(strHTML, "\\n+\\s+", string.Empty).Trim();
            strHTML = Regex.Replace(strHTML, "\\n", string.Empty).Trim();
            strHTML = Regex.Replace(strHTML, "\r\n", string.Empty).Trim();
            strHTML = Regex.Replace(strHTML, "\n", string.Empty).Trim();
            strHTML = strHTML.Replace("<noscript>", "");
            strHTML = strHTML.Replace("</noscript>", "");
            strHTML = ZipService.ZipHtmlString(strHTML);
            return strHTML;
        }
        public static void OpenUrlWithDefaultBrowser(string path_url)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", string.Format("/s /c \"start {0}\"", path_url));
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            Process.Start(startInfo);
        }
        public static void OpenFileWith(string path_file, string path_opener = "")
        {
            if (!string.IsNullOrEmpty(path_opener))
            {
                try
                {
                    Process.Start(path_opener, path_file);
                }
                catch
                {
                    throw new Exception("错误:\r\n无法使用以下打开方式来打开此文件！\r\n" + Path.GetFileName(path_opener));
                }
            }
            else
            {
                try
                {
                    Process.Start(path_file);
                }
                catch
                {
                    throw new Exception("错误:\r\n无法使用系统默认打开方式来打开此文件！");
                }
            }
        }
        public static List<string> SplitByLine(string text)
        {
            List<string> lines = new List<string>();
            byte[] array = Encoding.UTF8.GetBytes(text);
            using (MemoryStream stream = new MemoryStream(array))
            {
                using (var sr = new StreamReader(stream))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        lines.Add(line);
                        line = sr.ReadLine();
                    };
                }
            }
            return lines;
        }
        public static List<string> FileReadList(string filepath)
        {
            List<string> result = new List<string>();
            string strLine = string.Empty;
            FileStream fsrd = File.OpenRead(filepath);
            StreamReader sr = new StreamReader(fsrd, Encoding.UTF8);
            while ((strLine = sr.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(strLine) && strLine.Substring(0, 2) != "//")
                {
                    result.Add(strLine);
                }
            }
            sr.Close();
            fsrd.Close();
            return result;
        }
        public static void FileWrite(string filepath, string str2write)
        {
            FileStream fscr = File.Create(filepath);
            StreamWriter sw = new StreamWriter(fscr, Encoding.UTF8);
            sw.Write(str2write);
            sw.Close();
            sw.Dispose();
            fscr.Close();
            fscr.Dispose();
        }
        public static string FileRead(string filepath)
        {
            string result = string.Empty;
            FileStream fsrd = File.OpenRead(filepath);
            StreamReader sr = new StreamReader(fsrd, Encoding.UTF8);
            result = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            fsrd.Close();
            fsrd.Dispose();
            GC.Collect();
            return result;
        }
        public static void FileDel(string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static byte[] FileToByteArray(string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            byte[] buffer = new byte[(int)fileStream.Length];
            binaryReader.Read(buffer, 0, buffer.Length);
            binaryReader.Close();
            fileStream.Close();
            return buffer;
        }
        public static void FileReName(string fileOldPath, string fileNewPath)
        {
            try
            {
                File.Move(fileOldPath, fileNewPath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void CheckDir(string url, bool root)
        {
            if (root)
            {
                url = Environment.CurrentDirectory + url;
            }
            if (!Directory.Exists(url))
            {
                Directory.CreateDirectory(url);
            }
        }
        public static void DeleteShortcutOnDesktop(string AppName)
        {
            string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), AppName + ".lnk");
            if (File.Exists(shortcutPath))
            {
                File.Delete(shortcutPath);
            }
        }
        public static void DeleteShortcutOnStartup(string AppName)
        {
            string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), AppName + ".lnk");
            if (File.Exists(shortcutPath))
            {
                File.Delete(shortcutPath);
            }
        }
        public static void OpenFolder(string dirpath)
        {
            if (!Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }
            Process.Start("explorer.exe", dirpath);
        }
    }
}
