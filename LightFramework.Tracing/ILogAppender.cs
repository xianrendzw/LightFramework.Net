using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// ILogAppenderΪ����־��Ϣ���������豸�Ľӿ�
    /// </summary>
    public interface ILogAppender
    {
        /// <summary>
        /// ����־��Ϣ���������豸������
        /// </summary>
        /// <param name="metaLog">��־���ݷ��Ͷ���</param>
        void Append(MetaLog metaLog);
    }
}
