using Common.Application.Mappings;

namespace VS.Application;

public sealed class ApplicationMappingRegisterer : MappingRegisterer
{
    public ApplicationMappingRegisterer() : base(typeof(ApplicationMappingRegisterer).Assembly)
    {
    }
}