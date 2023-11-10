using AutoMapper;

namespace Common.Application.Mappings;

public interface IMapTo<T>
{
    void CreateMap(Profile profile)
    {
        var mapping = profile.CreateMap(GetType(), typeof(T));
        mapping.ValueTransformers.Add<string?>(value => value != null ? value.Trim() : null);
    }
}