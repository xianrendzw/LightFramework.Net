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
        LeftBracket = 0,

        /// <summary>
        /// ')'
        /// </summary>
        RightBracket = 1,

        /// <summary>
        /// '['
        /// </summary>
        LeftSquare = 2,

        /// <summary>
        /// ']'
        /// </summary>
        RightSquare = 3,

        /// <summary>
        /// '{'
        /// </summary>
        LeftCurly = 4,

        /// <summary>
        /// '}'
        /// </summary>
        RightCurly = 5,
    }
}
