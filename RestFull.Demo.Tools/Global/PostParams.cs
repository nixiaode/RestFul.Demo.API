namespace RestFull.Demo.Tools.Global
{
    /// <summary>
    /// Post参数泛型实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PostParams<T>
    {
        /// <summary>
        /// 泛型对象
        /// </summary>
        public T Body { get; set; }
    }
}
