using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LightFramework.Tracing
{
    /// <summary>
    /// TextLogAppender提供把日志信息输出到Txt类型文本文件的相关方法与数据。
    /// </summary>
    public class TextLogAppender : ILogAppender,ILogLayout
    {
        /// <summary>
        /// TextLogAppender的单件.
        /// </summary>
        public static readonly TextLogAppender Instance = new TextLogAppender();

        /// <summary>
        /// 构造函数。
        /// </summary>
        public TextLogAppender()
        {
        }

        /// <summary>
        /// 把日志信息输出到Txt类型文本文件的方法。
        /// </summary>
        /// <param name="metaLog">日志数据封送对象</param>
        public virtual void Append(MetaLog metaLog)
        {
            TxtMetaLog txtlog = metaLog as TxtMetaLog;
            string filePath = txtlog.Path + @"log\" + txtlog.LogDateTime.ToShortDateString();

            //如果目录不存在,则创建目录
            if (!Directory.Exists(filePath)) 
                Directory.CreateDirectory(filePath);
            //完整格式的文件路径
            string fullFileName = filePath + @"\" + txtlog.LogFileName;

            //写文件
            StreamWriter sw = new StreamWriter(fullFileName, true, Encoding.GetEncoding(metaLog.Encoding));
            sw.WriteLine(this.Format(metaLog));
            sw.Close();
        }

        #region ILogLayout Members

        /// <summary>
        /// 设置日志输出到文本文件中的版式。
        /// </summary>
        /// <param name="metaLog">日志数据封送对象</param>
        /// <returns>输出到设备中的文本格式</returns>
        public string Format(MetaLog metaLog)
        {
            string format = "[级别:{0}][时间:{1}],[信息:{2}]\r\n";
            return string.Format(format, metaLog.Level.Name,metaLog.LogDateTime.ToString(), metaLog.Message);
        }

        #endregion
    }
}
