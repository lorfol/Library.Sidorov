using AutoMapper;

namespace Library.App.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
        }
    }
}