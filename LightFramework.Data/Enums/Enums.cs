namespace LightFramework.Data
{
    /// <summary>
    /// SQL����е���������ö��
    /// </summary>
    public enum SortTypeEnum
    {
        /// <summary>
        /// ����
        /// </summary>
        ASC,

        /// <summary>
        /// ����
        /// </summary>
        DESC
    }

    /// <summary>
    /// SQL�����������
    /// </summary>
    public enum Condition : int
    {
        /// <summary>
        /// ����ҪWHERE�ؼ���
        /// </summary>
        Empty = 0,

        /// <summary>
        /// ��ҪWHERE�ؼ���
        /// </summary>
        Where = 1
    }
}
