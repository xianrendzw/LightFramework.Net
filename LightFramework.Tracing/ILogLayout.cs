using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Tracing
{
    /// <summary>
    /// ILogLayout�ṩ������־������豸�еİ�ʽ�Ľӿڡ�
    /// </summary>
    public interface ILogLayout
    {
        /// <summary>
        /// ������־������豸�еİ�ʽ
        /// </summary>
        /// <param name="metaLog">��־���ݷ��Ͷ���</param>
        /// <returns>������豸�е��ı���ʽ</returns>
        string Format(MetaLog metaLog);

    }
}
