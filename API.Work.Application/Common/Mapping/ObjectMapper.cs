using AutoMapper;

namespace API.Work.Application.Common.Mapping
{
    public static class ObjectMapper
    {
        public static IMapper Mapper { get; private set; }

        public static void Configure(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
