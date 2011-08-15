using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LightFramework.Core
{
    /// <summary>
    /// 创建,删除文件
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 生成文件。
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void CreateFile(string path, string content, Encoding encoding)
        {
            string dirPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            StreamWriter writer = new StreamWriter(path, false, encoding);
            writer.Write(content);
            writer.Close();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFile(string path)
        {
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
        }
    }
}