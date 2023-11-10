using System.Reflection;
using System.Runtime.Serialization;
using AutoMapper;

namespace Common.Application.Mappings;

public abstract class MappingRegisterer : Profile
{
    protected MappingRegisterer(Assembly scanAssembly)
    {
        RegisterMappingFromMarker(scanAssembly, typeof(IMapFrom<>), nameof(IMapFrom<object>.CreateMap));
        RegisterMappingFromMarker(scanAssembly, typeof(IMapTo<>), nameof(IMapTo<object>.CreateMap));
    }

    private void RegisterMappingFromMarker(
        Assembly scanAssembly,
        Type marker,
        string markerCreateMapMethodName
    )
    {
        var types = scanAssembly
            .GetExportedTypes()
            .Where(t => t.IsClass && (t.IsPublic || t.IsNested) && !t.IsAbstract)
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == marker))
            .ToList();

        foreach (var type in types)
        {
            var instance = FormatterServices.GetUninitializedObject(type);

            var mappings = type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == marker);

            foreach (var mapping in mappings)
            {
                var methodInfo = mapping.GetMethod(markerCreateMapMethodName);
                methodInfo!.Invoke(
                    instance,
                    new object[]
                    {
                        this
                    }
                );
            }
        }
    }
}