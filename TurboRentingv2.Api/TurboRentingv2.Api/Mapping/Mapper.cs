using TurboRentingv2.Api.Interfaces;

namespace TurboRentingv2.Api.Mapping
{
    public class Mapper : IMapper
    {
        private readonly AutoMapper.IMapper mapper;
        public Mapper(AutoMapper.IMapper mapper)
        {
            this.mapper = mapper;
        }


        public TDestination Map<TDestination>(object source) => mapper.Map<TDestination>(source);


        public object Map(object source, object destination) => mapper.Map(source, destination);


        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination) => mapper.Map(source, destination);
    }
}
