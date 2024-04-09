using System.Text;
using System.Text.RegularExpressions;

namespace Junior.Core.Service
{
    public class ZipService
    {
        /// <summary>
        /// 压缩文件/文件夹
        /// </summary>
        /// <param name="filePath">需要压缩的文件/文件夹路径</param>
        /// <param name="zipPath">压缩文件路径（zip后缀）</param>
        /// <param name="password">密码</param>
        /// <param name="filterExtenList">需要过滤的文件后缀名</param>
        public static bool ZipFile(string filePath, string zipPath, string password = "", List<string> filterExtenList = null)
        {
            bool value = false;
            try
            {
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(Encoding.UTF8))
                {
                    if (!string.IsNullOrWhiteSpace(password))
                    {
                        zip.Password = password;
                    }
                    if (Directory.Exists(filePath))
                    {
                        if (filterExtenList == null)
                            zip.AddDirectory(filePath);
                        else
                            AddDirectory(zip, filePath, filePath, filterExtenList);
                    }
                    else if (File.Exists(filePath))
                    {
                        zip.AddFile(filePath, "");
                    }
                    zip.Save(zipPath);
                }
                value = true;
            }
            catch (Exception ex)
            {
                throw new Exception($"[ZipService] {ex.Message}");
            }
            return value;
        }
        /// <summary>
        /// 添加文件夹
        /// </summary>
        /// <param name="zip">ZipFile对象</param>
        /// <param name="dirPath">需要压缩的文件夹路径</param>
        /// <param name="rootPath">根目录路径</param>
        /// <param name="filterExtenList">需要过滤的文件后缀名</param>
        public static void AddDirectory(Ionic.Zip.ZipFile zip, string dirPath, string rootPath, List<string> filterExtenList)
        {
            var files = Directory.GetFiles(dirPath);
            for (int i = 0; i < files.Length; i++)
            {
                if (filterExtenList == null || (filterExtenList != null && !filterExtenList.Any(d => Path.GetExtension(files[i]).Contains(d, StringComparison.OrdinalIgnoreCase))))
                {
                    zip.AddFile(files[i], Path.GetRelativePath(rootPath, dirPath));
                }
            }
            var dirs = Directory.GetDirectories(dirPath);
            for (int i = 0; i < dirs.Length; i++)
            {
                AddDirectory(zip, dirs[i], rootPath, filterExtenList);
            }
        }
        /// <summary>
        /// 压缩HTML
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string ZipHtmlString(string strHtml)
        {
            return Regex.Replace(strHtml, @"\s+(?=<)|\s+$|(?<=>)\s+", "");
        }
    }
}
