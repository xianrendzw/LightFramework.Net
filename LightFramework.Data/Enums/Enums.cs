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
    /// SQL��������Ӿ�����
    /// </summary>
    public enum SqlClause : int
    {
        /// <summary>
        /// �޴Ӿ�
        /// </summary>
        None = 0,

        /// <summary>
        /// WHERE�Ӿ�
        /// </summary>
        Where = 1,

        /// <summary>
        /// Having�Ӿ�
        /// </summary>
        Having = 2
    }

    /// <summary>
    /// ��������
    /// </summary>
    public enum Bracket : int
    {
        /// <summary>
        /// '('
        /// </summary>
        Left = 0,

        /// <summary>
        /// ')'
        /// </summary>
        Rgiht = 1,
    }

    /// <summary>
    /// Sql���ݲ��뷽����
    /// </summary>
    public enum SqlInsertMethod
    {
        /// <summary>
        /// ʹ��BulkCopy
        /// </summary>
        BulkCopy,

        /// <summary>
        /// ʹ��SQLServer2008�����ϰ汾��ֵ����,
        /// ��Ҫ�������ݿⴴ����Ŀ���ṹ��ͬ�ı��ұ�������Ϊtvps��
        /// </summary>
        TableValue
    }
}
