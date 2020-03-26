namespace AutoMapper
{
    public static class MapperConfigurationExpression
    {
        /// <summary>
        /// 添加双向映射
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="cfg"></param>
        public static void AddMap<TSource, TDestination>(this IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<TSource, TDestination>();
            cfg.CreateMap<TDestination, TSource>();
        }
    }
}
