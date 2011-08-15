using System.Collections.Generic;
using System.Data;

namespace LightFramework.Data
{
    /// <summary>
    /// ��ҳ��ѯ��¼���ࡣ
    /// </summary>
    /// <typeparam name="T">ͨ������</typeparam>
    public class PageData<T>
    {
        /// <summary>
        /// ��ѯ���صķ�ҳ��¼��
        /// </summary>
        private List<T> _recordSet = null;

        /// <summary>
        /// ��ҳ������
        /// </summary>
        private int _count;

        /// <summary>
        /// ��ǰ��ҳ�����ļ�¼����(�磺1-5)
        /// </summary>
        private string _currentRecordInterval;

        /// <summary>
        /// ��ȡ�����ò�ѯ���صķ�ҳ��¼��
        /// </summary>
        public List<T> RecordSet
        {
            get { return this._recordSet; }
            set { this._recordSet = value; }
        }

        /// <summary>
        /// ��ȡ�����÷��ϲ�ѯ�����ܼ�¼��
        /// </summary>
        public int Count
        {
            get { return this._count; }
            set { this._count = value; }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ��ҳ�����ļ�¼����(�磺1-5)
        /// </summary>
        public string CurrentRecordInterval
        {
            get { return this._currentRecordInterval; }
            set { this._currentRecordInterval = value; }
        }
    }
}
